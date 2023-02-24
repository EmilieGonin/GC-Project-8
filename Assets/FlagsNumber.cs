using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlagsNumber : MonoBehaviour
{
    public static FlagsNumber Instance { get; private set; }
    private int _count = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void Add()
    {
        _count += 1;
        transform.GetComponent<TextMeshPro>().text = _count.ToString();
    }

    public void Sub()
    {
        _count -= 1;
        transform.GetComponent<TextMeshPro>().text = _count.ToString();
    }
}
