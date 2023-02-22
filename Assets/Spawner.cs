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
    List<GameObject> bombs = new List<GameObject>();
    GameObject[,] cells;
    System.Random random = new System.Random();
    int max = 5;
    int min = -4;

    // Start is called before the first frame update
    void Start()
    {
        //Bombs
        for (int i = 0; i < 10; i++)
        {
            if (!addBomb())
            {
                addBomb();
            }
        }

        //Cells
        cells = new GameObject[max - min, max - min];
        for (int i = min; i < max; i++)
        {
            for (int j = min; j < max; j++)
            {
                GameObject newCell = Instantiate(cell, new Vector2(j, i), Quaternion.identity);
                cells[j + 4, i + 4] = newCell;

                GameObject text = newCell.transform.GetChild(2).gameObject;

                foreach (var bombItem in bombs)
                {
                    if (bombItem.transform.position == newCell.transform.position)
                    {
                        bombItem.transform.parent = newCell.transform;
                    }
                    else if (isAdjacent(bombItem.transform.position, newCell.transform.position))
                    {
                        int count = text.GetComponent<TextMeshPro>().text == "" ? 1 : int.Parse(text.GetComponent<TextMeshPro>().text) + 1;
                        text.GetComponent<TextMeshPro>().text = count.ToString();
                        //Changer couleur en fonction du chiffre

                        switch (count)
                        {
                            case 1:
                                text.GetComponent<TextMeshPro>().color = Color.cyan;
                                break;
                            case 2:
                                text.GetComponent<TextMeshPro>().color = Color.green;
                                break;
                            case 3:
                                text.GetComponent<TextMeshPro>().color = Color.red;
                                break;
                            case 4:
                                text.GetComponent<TextMeshPro>().color = Color.blue;
                                break;
                            case 5:
                                text.GetComponent<TextMeshPro>().color = Color.magenta;
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

    bool addBomb()
    {
        int x = random.Next(min, max);
        int y = random.Next(min, max);
        GameObject newBomb = Instantiate(bomb, new Vector2(x, y), Quaternion.identity);

        //On v�rifie si l'emplacement choisis n'est pas d�j� pris par une bombe
        foreach (var bombItem in bombs)
        {
            if (bombItem.transform.position == newBomb.transform.position)
            {
                Destroy(newBomb);
                return false;
            }
        }

        bombs.Add(newBomb);
        newBomb.SetActive(false);
        return true;
    }

    bool isAdjacent(Vector2 bombPos, Vector2 cellPos)
    {
        for (var i = -1; i <= 1; i++)
        {
            for (var j = -1; j <= 1; j++)
            {
                Debug.Log("i = " + i + "\n");
                Debug.Log("j = " + j + "\n");
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
