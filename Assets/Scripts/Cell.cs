using TMPro;
using UnityEngine;
using Color = UnityEngine.Color;

public class Cell : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] SpriteRenderer _square;
    [SerializeField] GameObject _borders;
    [SerializeField] TextMeshPro _text;
    [SerializeField] GameObject _flag;

    [Header("Colors")]
    [SerializeField] Color _colorHidden;
    [SerializeField] Color _colorRevealed;
    [SerializeField] Color _colorFlag;
    [SerializeField] Color _colorBomb;
    [SerializeField] Color _colorError;

    [Header("Sound Effects")]
    [SerializeField] AudioSource _clickSound;
    [SerializeField] AudioSource _flagSound;
    [SerializeField] AudioSource _flagDestroySound;

    private Bomb _bomb;
    private bool _isRevealed;

    private void Awake()
    {
        _isRevealed = false;
    }

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
        if (Input.GetMouseButtonDown(1) && !_isRevealed && Spawner.Instance.IsPlaying)
        {
            if (_flag.activeSelf)
            {
                _flag.SetActive(!_flag.activeSelf);
                _square.color = _colorHidden;
                _flagDestroySound.Play();
                Parameters.Instance.SubFlag();
            }
            else if (!Parameters.Instance.FlagLimitReached())
            {
                _flag.SetActive(!_flag.activeSelf);
                _square.color = _colorFlag;
                _flagSound.Play();
                Parameters.Instance.AddFlag();

                if (_bomb != null)
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
            _isRevealed = true;
            Spawner.Instance.AddRevealedCell(gameObject);

            if (_bomb || Spawner.Instance.IsPlaying)
            {
                _borders.SetActive(false);
            }

            if (_bomb)
            {
                _bomb.gameObject.SetActive(true);

                if (Spawner.Instance.IsPlaying)
                {
                    _square.color = _colorBomb;
                    _bomb.PlaySound();
                    Debug.Log("game over");
                }
                else
                {
                    _square.color = _colorRevealed;
                }

                Spawner.Instance.RevealAll();

            }
            else if (Spawner.Instance.IsPlaying)
            {
                _clickSound.Play();
                _square.color = _colorRevealed;
                _text.gameObject.SetActive(true);
            }

            CheckReveal();
            Spawner.Instance.CheckWin();
        }
        else if (!_bomb && !_isRevealed && !Spawner.Instance.IsPlaying && _flag.activeSelf)
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

    public void SetBomb(Bomb bomb)
    {
        _bomb = bomb;
    }
}
