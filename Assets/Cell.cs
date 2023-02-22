using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using Color = UnityEngine.Color;

public class Cell : MonoBehaviour
{
    [SerializeField] bool isRevealed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    private void OnMouseDown()
    {
        GameObject spawner = GameObject.Find("Spawner");
        if (!spawner.GetComponent<Spawner>().HasStarted())
        {
            Debug.Log("init");
            spawner.GetComponent<Spawner>().Init(gameObject);
        }
        
        if (!isRevealed)
        {
            isRevealed = true;
            SpriteRenderer square = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

            if (transform.childCount == 5)
            {
                square.color = Color.red;
                transform.GetChild(4).gameObject.SetActive(true);
                Debug.Log("game over");
            }
            else
            {
                square.color = Color.white;
                transform.GetChild(2).gameObject.SetActive(true);
            }

            if (transform.GetChild(3).gameObject.activeSelf)
            {
                transform.GetChild(3).gameObject.SetActive(false);
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && !isRevealed)
        {
            Debug.Log("flag");
            if (transform.GetChild(3).gameObject.activeSelf)
            {
                transform.GetChild(3).gameObject.SetActive(false);
            } else
            {
                transform.GetChild(3).gameObject.SetActive(true);
            }
        }
    }
}
