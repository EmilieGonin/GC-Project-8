using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Parameters : MonoBehaviour
{
    [SerializeField] TextMeshPro _flags;
    [SerializeField] TextMeshPro _bombs;

    public static Parameters Instance { get; private set; }
    private int _flagsNumber;
    private int _bombsNumber;

    private void Awake()
    {
        Instance = this;
    }

    public void SetBombs(int count)
    {
        _bombsNumber = count;
        _bombs.text = _bombsNumber.ToString();
    }

    public void AddFlag()
    {
        _flagsNumber += 1;
        _flags.text = _flagsNumber.ToString();
    }

    public void SubFlag()
    {
        _flagsNumber -= 1;
        _flags.text = _flagsNumber.ToString();
    }

    public bool FlagLimitReached()
    {
        return _flagsNumber == _bombsNumber;
    }
}