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
            int x = random.Next(min, max);
            Debug.Log(x);
            int y = random.Next(min, max);
            Debug.Log(y);
            GameObject newBomb = Instantiate(bomb, new Vector2(x, y), Quaternion.identity);
            bombs.Add(newBomb);
            //Vérifier s'il existe déjà une bombe
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

    bool isAdjacent(Vector2 bombPos, Vector2 cellPos)
    {
        if (bombPos == cellPos + new Vector2(1, 1) ||
            bombPos == cellPos + new Vector2(1, 0) ||
            bombPos == cellPos + new Vector2(0, 1) ||
            bombPos == cellPos + new Vector2(-1, -1) ||
            bombPos == cellPos + new Vector2(-1, 0) ||
            bombPos == cellPos + new Vector2(0, -1) ||
            bombPos == cellPos + new Vector2(-1, 1) ||
            bombPos == cellPos + new Vector2(1, -1))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
