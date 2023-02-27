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
    private bool _isRevealed = false;

    private void OnMouseDown()
    {
        if (!Spawner.Instance.HasStarted())
        {
            Spawner.Instance.Init(gameObject);
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
        if (!_isRevealed)
        {
        ClickSound.Play();
            _isRevealed = true;
            Spawner.Instance.AddRevealedCell(gameObject);
            Bomb bomb = transform.GetComponentInChildren<Bomb>(true);

            if (bomb != null)
            {
                if (Spawner.Instance.IsPlaying)
                {
                    _square.color = _colorBomb;
                    Debug.Log("game over");
                }
                else if (!_flag.activeSelf)
                {
                    _square.color = _colorRevealed;
                }

                if (Spawner.Instance.IsPlaying || (!Spawner.Instance.IsPlaying && !_flag.activeSelf))
                {
                    bomb.gameObject.SetActive(true);
                }

                Spawner.Instance.RevealAll();

            }
            else if (!Spawner.Instance.IsPlaying && _flag.activeSelf)
            {
                _square.color = _colorError;
            }
            else if (Spawner.Instance.IsPlaying)
            {
                _square.color = _colorRevealed;
                _text.gameObject.SetActive(true);
            }

            if (Spawner.Instance.IsPlaying && _flag.activeSelf)
            {
                _flag.SetActive(false);
            }

            CheckReveal();
            Spawner.Instance.CheckWin();
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
}
