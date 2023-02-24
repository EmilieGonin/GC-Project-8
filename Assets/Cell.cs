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

    private bool isRevealed = false;

    void OnMouseDown()
    {
        if (!Spawner.Instance.HasStarted())
        {
            Debug.Log("init");
            Spawner.Instance.Init(gameObject);
        }

        Reveal();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && !isRevealed)
        {
            if (_flag.activeSelf)
            {
                _square.color = _colorFlag;
                _flag.SetActive(false);
                FlagsNumber.Instance.Sub();
            }
            else
            {
                _square.color = _colorHidden;
                _flag.SetActive(true);
                FlagsNumber.Instance.Add();
            }
        }
    }

    public void Reveal()
    {
        if (!isRevealed)
        {
            isRevealed = true;
            Bomb bomb = transform.GetComponentInChildren<Bomb>(true);

            if (bomb != null)
            {
                _square.color = _colorBomb;
                bomb.gameObject.SetActive(true);
                Debug.Log("game over");
            }
            else
            {
                _square.color = _colorRevealed;
                _text.gameObject.SetActive(true);
            }

            if (_flag.activeSelf)
            {
                _flag.SetActive(false);
            }

            CheckReveal();
        }
    }

    void CheckReveal()
    {
        if (_text.text == "")
        {
            Spawner.Instance.RevealAll(gameObject);
        }
    }
}
