using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance { get; private set; }

    [Header("Ref")]
    [SerializeField] GameObject _cell;
    [SerializeField] GameObject _bomb;
    [SerializeField] Camera _camera;

    [Header("Settings")]
    [SerializeField] int _max;
    [SerializeField] int _min;
    [SerializeField] int _bombsNumber;
    [SerializeField] bool _unsafeMode;
    [SerializeField] bool _luckyMode;

    [Header("Colors")]
    [SerializeField] Color _colorOne = new Color32(18, 59, 255, 255);
    [SerializeField] Color _colorTwo = new Color32(1, 154, 30, 255);
    [SerializeField] Color _colorThree = Color.red;
    [SerializeField] Color _colorFour = new Color32(1, 4, 154, 255);
    [SerializeField] Color _colorFive = new Color(109, 11, 11, 255);
    [SerializeField] Color _colorSix = Color.yellow;
    [SerializeField] Color _colorSeven = Color.magenta;
    [SerializeField] Color _colorEight = Color.yellow;

    public bool IsPlaying { get; private set; }
    private List<Bomb> _bombs;
    private GameObject[,] _cells;
    private List<GameObject> _revealedCells;
    private List<GameObject> _flagedCells;
    private System.Random _random;
    private bool _started;
    private Color[] _colors;

    private void Awake()
    {
        Instance = this;
        _bombs = new();
        _revealedCells = new();
        _flagedCells = new();
        _random = new();
        _started = false;
        _colors = new Color[] { _colorOne, _colorTwo, _colorThree, _colorFour, _colorFive, _colorSix, _colorSeven, _colorEight };
    }

    private void Start()
    {
        IsPlaying = true;
        SetDifficulty();
        SetGameMode();
        Parameters.Instance.SetBombs(_bombsNumber);
        _cells = new GameObject[_max - _min, _max - _min];

        for (int i = _min; i < _max; i++)
        {
            for (int j = _min; j < _max; j++)
            {
                GameObject cell = Instantiate(_cell, new Vector2(j, i), Quaternion.identity);
                cell.name = $"{i}, {j} Cell";
                cell.transform.GetComponentInChildren<TextMeshPro>().gameObject.SetActive(false);
                _cells[j - _min, i - _min] = cell;
            }
        }
    }

    public void Init(GameObject clickedCell)
    {
        Timer.Instance.Run();
        _started = true;

        //Bombs
        while (_bombs.Count < _bombsNumber)
        {
            AddBomb(clickedCell);
        }

        //Cells
        for (int i = _min; i < _max; i++)
        {
            for (int j = _min; j < _max; j++)
            {
                GameObject cell = _cells[j - _min, i - _min];
                TextMeshPro text = cell.GetComponentInChildren<TextMeshPro>(true);

                foreach (var bombItem in _bombs)
                {
                    if (bombItem.transform.position == cell.transform.position)
                    {
                        bombItem.transform.parent = cell.transform;
                        cell.GetComponent<Cell>().SetBomb(bombItem);
                    }
                    else if (IsAdjacent(bombItem.transform.position, cell.transform.position))
                    {
                        int count = text.text == "" ? 1 : int.Parse(text.text) + 1;
                        text.text = count.ToString();
                        text.color = _colors[count - 1];
                    }
                }
            }
        }
    }

    public void RevealAllAdjacent(GameObject clickedObject)
    {
        foreach (var cell in _cells)
        {
            if (IsAdjacent(clickedObject.transform.position, cell.transform.position))
            {
                cell.GetComponent<Cell>().Reveal();
            }
        }
    }

    public bool IsAllRevealedAndFlaged()
    {
        return _revealedCells.Count + _flagedCells.Count == _cells.Length;
    }

    private void SetDifficulty()
    {
        int difficulty = GameData.Instance? GameData.Instance.GameDifficulty : 1;

        switch (difficulty)
        {
            case 0:
            case 1:
                _max = 5;
                _min = -4;
                _bombsNumber = 15;
                _camera.orthographicSize = 5;
                break;
            case 2:
                _max = 10;
                _min = -9;
                _bombsNumber = 50;
                _camera.orthographicSize = 10.5f;
                break;
            case 3:
                _max = 15;
                _min = -14;
                _bombsNumber = 150;
                _camera.orthographicSize = 16;
                break;
        }
    }

    private void SetGameMode()
    {
        int mode = GameData.Instance? GameData.Instance.GameMode : 0;

        switch (mode)
        {
            case 0:
                _luckyMode = false;
                _unsafeMode = false;
                break;
            case 1:
                _luckyMode = true;
                _unsafeMode = false;
                break;
            case 2:
                _luckyMode = false;
                _unsafeMode = true;
                break;
        }
    }

    public bool HasStarted()
    {
        return _started;
    }

    private bool AddBomb(GameObject clickedCell)
    {
        int x = _random.Next(_min, _max);
        int y = _random.Next(_min, _max);
        GameObject bomb = Instantiate(_bomb, new Vector2(x, y), Quaternion.identity);
        _bomb.name = $"{x}, {y} Bomb";

        if (!_unsafeMode && !_luckyMode && IsAdjacent(bomb.transform.position, clickedCell.transform.position))
        {
            Destroy(bomb);
            return false;
        }
        else if (!_unsafeMode && (bomb.transform.position == clickedCell.transform.position)) {
            Destroy(bomb);
            return false;
        }

        foreach (var bombItem in _bombs)
        {
            if (bomb.transform.position == bombItem.transform.position)
            {
                Destroy(bomb);
                return false;
            }
        }

        _bombs.Add(bomb.GetComponent<Bomb>());
        bomb.SetActive(false);
        return true;
    }

    private bool IsAdjacent(Vector2 bombPos, Vector2 cellPos)
    {
        for (var i = -1; i <= 1; i++)
        {
            for (var j = -1; j <= 1; j++)
            {
                if (bombPos == cellPos + new Vector2(i, j))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void AddFlagedCell(GameObject cell)
    {
        _flagedCells.Add(cell);
    }

    public void AddRevealedCell(GameObject cell)
    {
        _revealedCells.Add(cell);
    }

    public void CheckWin()
    {
        bool hasWin = true;

        if (Parameters.Instance.FlagLimitReached() && IsAllRevealedAndFlaged())
        {
            foreach (var cell in _flagedCells)
            {
                if (!cell.GetComponent<Cell>().HasBomb())
                {
                    hasWin = false;
                }
            }

            if (hasWin)
            {
                Win();
            }
        }
    }

    private void Win()
    {
        Debug.Log("win");
        End(true);
    }

    public void GameOver()
    {
        Debug.Log("game over");
        End(false);

        foreach (var cell in _cells)
        {
            cell.GetComponent<Cell>().Reveal();
        }
    }

    private void End(bool win)
    {
        IsPlaying = false;
        Timer.Instance.Pause();
        Parameters.Instance.ShowMessage(win);
    }
}
