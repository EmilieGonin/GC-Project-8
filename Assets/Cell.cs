using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using Color = UnityEngine.Color;

public class Cell : MonoBehaviour
{
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
        SpriteRenderer square = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

        if (transform.childCount == 4)
        {
            square.color = Color.red;
            Debug.Log("game over");
        } else
        {
            square.color = Color.gray;
            transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}
