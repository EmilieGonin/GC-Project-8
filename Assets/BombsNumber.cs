using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BombsNumber : MonoBehaviour
{
    public static BombsNumber Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void SetCount(int count)
    {
        transform.GetComponent<TextMeshPro>().text = count.ToString();
    }
}
