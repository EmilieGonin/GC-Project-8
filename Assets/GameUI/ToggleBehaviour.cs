using UnityEngine;

public class ToggleBehaviour : MonoBehaviour
{
    public int ToggledValue { get; set; }
    public int ToggledMode {get; set; }

    private void Awake()
    {
        ToggledValue = 1;
        ToggledMode = 1;
    }

    //Difficulty Toggles
    public void SetEasy()
    {
        ToggledValue = 1;
        SetDifficulty();
    }
    public void SetMedium()
    {
        ToggledValue = 2;
        SetDifficulty();
    }
    public void SetHard()
    {
        ToggledValue = 3;
        SetDifficulty();
    }

    private void SetDifficulty()
    {
        Difficulty.Instance.SetDifficulty(ToggledValue);
    }

    //Mode Toggles
    public void SetNormal()
    {
        ToggledMode = 0;
        GameMode.Instance.SetGameMode(0);
    }

    public void SetLucky()
    {
        ToggledMode = 1;
        GameMode.Instance.SetGameMode(1);
    }

    public void SetUnsafe()
    {
        ToggledMode = 2;
        GameMode.Instance.SetGameMode(2);
    }
}
