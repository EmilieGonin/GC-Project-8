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
    [SerializeField] GameObject cell;
    [SerializeField] GameObject bomb;
    [SerializeField] int max = 10;
    [SerializeField] int min = -9;
    [SerializeField] int bombsNumber = 50;
    List<GameObject> bombs = new List<GameObject>();
    GameObject[,] cells;
    System.Random random = new System.Random();
    bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        cells = new GameObject[max - min, max - min];
        for (int i = min; i < max; i++)
        {
            for (int j = min; j < max; j++)
            {
                GameObject newCell = Instantiate(cell, new Vector2(j, i), Quaternion.identity);
                cells[j - min, i - min] = newCell;
                GameObject text = newCell.transform.GetChild(2).gameObject;
                text.SetActive(false);
            }
        }
    }

    public void Init(GameObject clickedCell)
    {
        started = true;

        //Bombs
        for (int i = 0; i < bombsNumber; i++)
        {
            if (!AddBomb(clickedCell))
            {
                AddBomb(clickedCell);
            }
        }

        //Cells
        for (int i = min; i < max; i++)
        {
            for (int j = min; j < max; j++)
            {
                GameObject currentCell = cells[j - min, i - min];

                GameObject text = currentCell.transform.GetChild(2).gameObject;

                foreach (var bombItem in bombs)
                {
                    if (bombItem.transform.position == currentCell.transform.position)
                    {
                        bombItem.transform.parent = currentCell.transform;
                    }
                    else if (IsAdjacent(bombItem.transform.position, currentCell.transform.position))
                    {
                        int count = text.GetComponent<TextMeshPro>().text == "" ? 1 : int.Parse(text.GetComponent<TextMeshPro>().text) + 1;
                        text.GetComponent<TextMeshPro>().text = count.ToString();

                        switch (count)
                        {
                            case 1:
                                text.GetComponent<TextMeshPro>().color = new Color32(18, 59, 255, 255);
                                break;
                            case 2:
                                text.GetComponent<TextMeshPro>().color = new Color32(1, 154, 30, 255);
                                break;
                            case 3:
                                text.GetComponent<TextMeshPro>().color = Color.red;
                                break;
                            case 4:
                                text.GetComponent<TextMeshPro>().color = new Color32(1, 4, 154, 255);
                                break;
                            case 5:
                                text.GetComponent<TextMeshPro>().color = new Color(109, 11, 11, 255);
                                break;
                            case 6:
                                text.GetComponent<TextMeshPro>().color = Color.yellow;
                                break;
                        }
                    }
                }

                text.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool HasStarted()
    {
        return started;
    }

    bool AddBomb(GameObject clickedCell)
    {
        int x = random.Next(min, max);
        int y = random.Next(min, max);
        GameObject newBomb = Instantiate(bomb, new Vector2(x, y), Quaternion.identity);

        //On v�rifie si l'emplacement choisis n'est pas d�j� pris par une bombe
        foreach (var bombItem in bombs)
        {
            if (newBomb.transform.position == bombItem.transform.position)
            {
                Destroy(newBomb);
                return false;
            }
        }

        if (IsAdjacent(newBomb.transform.position, clickedCell.transform.position)) {
            Destroy(newBomb);
            return false;
        }

        bombs.Add(newBomb);
        newBomb.SetActive(false);
        return true;
    }

    bool IsAdjacent(Vector2 bombPos, Vector2 cellPos)
    {
        for (var i = -1; i <= 1; i++)
        {
            for (var j = -1; j <= 1; j++)
            {
                //Debug.Log("i = " + i + "\n");
                //Debug.Log("j = " + j + "\n");
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
