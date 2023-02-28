using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }

    [SerializeField] TextMeshPro _text;
    [SerializeField] Color _colorStarted;
    [SerializeField] Color _colorPaused;
    private bool _started;
    private float _seconds;
    private int _minutes;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Pause();
    }

    private void Update()
    {
        if (_started)
        {
            int seconds = Mathf.FloorToInt(_seconds += Time.deltaTime % 60);

            if (seconds == 60)
            {
                _seconds = 0;
                _minutes++;
            }

            string timer = $"{(_minutes < 10 ? 0 : "")}{_minutes}:{(seconds < 10 ? 0 : "")}{seconds}";
            _text.text = timer;
        }
    }

    public void Run()
    {
        _started = true;
        _text.color = _colorStarted;
    }

    public void Pause()
    {
        _started = false;
        _text.color = _colorPaused;
    }
}
