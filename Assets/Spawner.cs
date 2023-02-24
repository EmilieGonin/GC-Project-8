using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.UI.Image;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance { get; private set; }

    [Header("Ref")]
    [SerializeField] GameObject _cell;
    [SerializeField] GameObject _bomb;

    [Header("Settings")]
    [SerializeField] int _max = 5;
    [SerializeField] int _min = -4;
    [SerializeField] int _bombsNumber = 15;

    [Header("Colors")]
    [SerializeField] Color _colorOne;
    [SerializeField] Color _colorTwo;
    [SerializeField] Color _colorThree;
    [SerializeField] Color _colorFour;
    [SerializeField] Color _colorFive;
    [SerializeField] Color _colorSix;
    [SerializeField] Color _colorSeven;
    [SerializeField] Color _colorEight;

    public Camera camera;

    private List<GameObject> _bombs = new List<GameObject>();
    private GameObject[,] _cells;
    private System.Random _random = new System.Random();
    private bool _started = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        BombsNumber.Instance.SetCount(_bombsNumber);
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
        _started = true;

        //Bombs
        for (int i = 0; i < _bombsNumber; i++)
        {
            if (!AddBomb(clickedCell))
            {
                AddBomb(clickedCell);
            }
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

    public void RevealAll(GameObject clickedObject)
    {
        foreach (var cell in _cells)
        {
            if (IsAdjacent(clickedObject.transform.position, cell.transform.position))
            {
                cell.GetComponent<Cell>().Reveal();
            }
        }
    }

    private void SetDifficulty(int difficulty)
    {
        //Move FlagsNumber & BombsNumber depending on difficulty
        switch (difficulty)
        {
            case 1:
                _max = 5;
                _min = -4;
                _bombsNumber = 15;
                camera.orthographicSize = 5;
                break;
            case 2:
                _max = 10;
                _min = -9;
                _bombsNumber = 50;
                camera.orthographicSize = 10;
                break;
            case 3:
                _max = 15;
                _min = -14;
                _bombsNumber = 150;
                camera.orthographicSize = 15;
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
        Debug.Log("x :" + x);
        Debug.Log("y :" + y);
        GameObject bomb = Instantiate(_bomb, new Vector2(x, y), Quaternion.identity);
        _bomb.name = $"{x}, {y} Bomb";

        //On v�rifie si l'emplacement choisis n'est pas d�j� pris par une bombe
        foreach (var bombItem in _bombs)
        {
            if (bomb.transform.position == bombItem.transform.position)
            {
                Debug.Log("position already taken");
                Destroy(bomb);
                return false;
            }
        }

        if (IsAdjacent(bomb.transform.position, clickedCell.transform.position)) {
            Debug.Log("position not safe");
            Destroy(bomb);
            return false;
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
                else
                {
                    
                }
            }
        }
        return false;
    }
}
