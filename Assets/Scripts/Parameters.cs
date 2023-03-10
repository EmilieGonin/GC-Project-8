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
    [SerializeField] GameObject _scoreboard;

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
        _scoreboard.SetActive(true);
        _message.gameObject.SetActive(true);
        _message.color = win ? _colorWin : _colorLose;
        _message.text = win ? _textWin : _textLose;
    }
}
