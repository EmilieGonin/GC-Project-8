using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using Color = UnityEngine.Color;

public class Cell : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] SpriteRenderer _square;
    [SerializeField] TextMeshPro _text;
    [SerializeField] GameObject _flag;

    [Header("Colors")]
    [SerializeField] Color _colorHidden;
    [SerializeField] Color _colorRevealed;
    [SerializeField] Color _colorFlag;
    [SerializeField] Color _colorBomb;
    [SerializeField] Color _colorError;

    [Header("Sound Effects")]
    public AudioSource ClickSound;
    public AudioSource FlagSound;
    public AudioSource FlagDestroySound;

    private bool _bomb = false;
    private bool _isRevealed = false;

    private void OnMouseDown()
    {
        if (!Spawner.Instance.HasStarted())
        {
            Spawner.Instance.Init(gameObject);
        }

        if (Spawner.Instance.IsPlaying)
        {
            Reveal();
        }

    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && !Parameters.Instance.FlagLimitReached() && !_isRevealed && Spawner.Instance.IsPlaying)
        {
            if (_flag.activeSelf)
            {
                _square.color = _colorHidden;
                FlagDestroySound.Play();
                _flag.SetActive(false);
                Parameters.Instance.SubFlag();
            }
            else
            {
                _square.color = _colorFlag;
                FlagSound.Play();
                _flag.SetActive(true);
                Parameters.Instance.AddFlag();
                Bomb bomb = transform.GetComponentInChildren<Bomb>(true);

                if (bomb != null)
                {
                    Spawner.Instance.AddFlagedCell(gameObject);
                }
            }

            Spawner.Instance.CheckWin();
        }
    }

    public void Reveal()
    {
        if (!_isRevealed && !_flag.activeSelf)
        {
            ClickSound.Play();
            _isRevealed = true;
            Spawner.Instance.AddRevealedCell(gameObject);

            if (_bomb)
            {
                if (Spawner.Instance.IsPlaying)
                {
                    _square.color = _colorBomb;
                    Debug.Log("game over");
                }
                else
                {
                    _square.color = _colorRevealed;
                }

                transform.GetComponentInChildren<Bomb>(true).gameObject.SetActive(true);
                Spawner.Instance.RevealAll();

            }
            else if (Spawner.Instance.IsPlaying)
            {
                _square.color = _colorRevealed;
                _text.gameObject.SetActive(true);
            }

            CheckReveal();
            Spawner.Instance.CheckWin();
        }
        else if (_bomb && !_isRevealed && !Spawner.Instance.IsPlaying && _flag.activeSelf)
        {
            _square.color = _colorError;
        }
    }

    private void CheckReveal()
    {
        if (_text.text == "")
        {
            Spawner.Instance.RevealAllAdjacent(gameObject);
        }
    }

    public bool IsRevealed()
    {
        return _isRevealed;
    }

    public void HasBomb()
    {
        _bomb = true;
    }
}
