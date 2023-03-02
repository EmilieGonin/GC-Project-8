using UnityEngine;

public class ToggleBehaviour : MonoBehaviour
{
    private int _toggledValue;
    private int _toggledMode;

    private void Awake()
    {
        _toggledValue = 1;
        _toggledMode = 0;
    }

    //Difficulty Toggles
    public void SetEasy()
    {
        _toggledValue = 1;
        SetDifficulty();
    }
    public void SetMedium()
    {
        _toggledValue = 2;
        SetDifficulty();
    }
    public void SetHard()
    {
        _toggledValue = 3;
        SetDifficulty();
    }

    private void SetDifficulty()
    {
        GameData.Instance.SetDifficulty(_toggledValue);
    }

    //Mode Toggles
    public void SetNormal()
    {
        _toggledMode = 0;
        SetGameMode();
    }

    public void SetLucky()
    {
        _toggledMode = 1;
        SetGameMode();
    }

    public void SetUnsafe()
    {
        _toggledMode = 2;
        SetGameMode();
    }

    private void SetGameMode()
    {
        GameData.Instance.SetGameMode(_toggledMode);
    }
}
