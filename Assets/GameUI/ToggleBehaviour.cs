using UnityEngine;

public class ToggleBehaviour : MonoBehaviour
{
    public int ToggledValue { get; set; }

    private void Awake()
    {
        ToggledValue = 1;
    }

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
}
