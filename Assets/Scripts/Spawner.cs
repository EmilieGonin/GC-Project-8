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
    [SerializeField] int _max = 5;
    [SerializeField] int _min = -4;
    [SerializeField] int _bombsNumber = 15;
    [SerializeField] bool _unsafeMode = false;
    [SerializeField] bool _luckyMode = false;

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
    private List<GameObject> _bombs = new List<GameObject>();
    private GameObject[,] _cells;
    private List<GameObject> _revealedCells;
    private List<GameObject> _flagedCells;
    private System.Random _random = new System.Random();
    private bool _started = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        IsPlaying = true;
        SetDifficulty();
        Parameters.Instance.SetBombs(_bombsNumber);
        _cells = new GameObject[_max - _min, _max - _min];
        _revealedCells = new List<GameObject>();
        _flagedCells = new List<GameObject>();

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
                        cell.GetComponent<Cell>().HasBomb();
                    }
                    else if (IsAdjacent(bombItem.transform.position, cell.transform.position))
                    {
                        int count = text.text == "" ? 1 : int.Parse(text.text) + 1;
                        text.text = count.ToString();

                        switch (count)
                        {
                            case 1:
                                text.color = _colorOne;
                                break;
                            case 2:
                                text.color = _colorTwo;
                                break;
                            case 3:
                                text.color = _colorThree;
                                break;
                            case 4:
                                text.color = _colorFour;
                                break;
                            case 5:
                                text.color = _colorFive;
                                break;
                            case 6:
                                text.color = _colorSix;
                                break;
                            case 7:
                                text.color = _colorSeven;
                                break;
                            case 8:
                                text.color = _colorEight;
                                break;
                        }
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

    public void RevealAll()
    {
        IsPlaying = false;
        Timer.Instance.Pause();
        foreach (var cell in _cells)
        {
            cell.GetComponent<Cell>().Reveal();
        }
    }

    public bool IsAllRevealedAndFlaged()
    {
        return _revealedCells.Count + _flagedCells.Count == _cells.Length;
    }

    private void SetDifficulty()
    {
        int difficulty = Difficulty.Instance != null ? Difficulty.Instance.GameDifficulty : 1;

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
                _camera.orthographicSize = 10;
                break;
            case 3:
                _max = 15;
                _min = -14;
                _bombsNumber = 150;
                _camera.orthographicSize = 15;
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
            //Debug.Log("position not safe");
            Destroy(bomb);
            return false;
        }
        else if (!_unsafeMode && (bomb.transform.position == clickedCell.transform.position)) {
            //Debug.Log("position not safe");
            Destroy(bomb);
            return false;
        }

        foreach (var bombItem in _bombs)
        {
            if (bomb.transform.position == bombItem.transform.position)
            {
                //Debug.Log("position already taken");
                Destroy(bomb);
                return false;
            }
        }


        _bombs.Add(bomb);
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
                Bomb bomb = cell.transform.GetComponentInChildren<Bomb>(true);

                if (bomb == null)
                {
                    hasWin = false;
                }
            }

            if (hasWin)
            {
                Debug.Log("win");
                IsPlaying = false;
                Timer.Instance.Pause();
            }
        }
    }

    private void UnsafeMode(bool parameter)
    {
        _unsafeMode = parameter;
    }

    private void LuckyMode(bool parameter)
    {
        _luckyMode = parameter;
    }
}
