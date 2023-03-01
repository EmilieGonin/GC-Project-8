using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Parameters : MonoBehaviour
{
    public static Parameters Instance { get; private set; }

    [Header("Messages")]
    [SerializeField] string _textWin;
    [SerializeField] string _textLose;

    [Header("Ref")]
    [SerializeField] TextMeshPro _flags;
    [SerializeField] TextMeshPro _bombs;
    [SerializeField] TextMeshPro _message;

    [Header("Colors")]
    [SerializeField] Color _colorWin;
    [SerializeField] Color _colorLose;

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

    public void ShowMessage(bool win)
    {
        _message.gameObject.SetActive(true);

        if (win)
        {
            _message.color = _colorWin;
            _message.text = _textWin;
        }
        else
        {
            _message.color = _colorLose;
            _message.text = _textLose;
        }
    }
}
