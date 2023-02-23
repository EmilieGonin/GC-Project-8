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

    void OnMouseDown()
    {
        GameObject spawner = GameObject.Find("Spawner");

        if (!spawner.GetComponent<Spawner>().HasStarted())
        {
            Debug.Log("init");
            spawner.GetComponent<Spawner>().Init(gameObject);
        }

        Reveal();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && !isRevealed)
        {
            GameObject flagsNumber = GameObject.Find("FlagsNumber");
            SpriteRenderer square = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

            if (transform.GetChild(3).gameObject.activeSelf)
            {
                square.color = new Color32(62, 136, 221, 255);
                int count = int.Parse(flagsNumber.GetComponent<TextMeshPro>().text) - 1;
                flagsNumber.GetComponent<TextMeshPro>().text = count.ToString();
                transform.GetChild(3).gameObject.SetActive(false);
            }
            else
            {
                square.color = new Color32(32, 95, 233, 255);
                int count = flagsNumber.GetComponent<TextMeshPro>().text == "" ? 1 : int.Parse(flagsNumber.GetComponent<TextMeshPro>().text) + 1;
                flagsNumber.GetComponent<TextMeshPro>().text = count.ToString();

                transform.GetChild(3).gameObject.SetActive(true);
            }
        }
    }

    public void Reveal()
    {
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

            checkReveal();
        }
    }

    void checkReveal()
    {
        GameObject spawner = GameObject.Find("Spawner");
        GameObject text = transform.GetChild(2).gameObject;

        if (text.GetComponent<TextMeshPro>().text == "")
        {
            spawner.GetComponent<Spawner>().RevealAll(gameObject);
        }
    }
}
