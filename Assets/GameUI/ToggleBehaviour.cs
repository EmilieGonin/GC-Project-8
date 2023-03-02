using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBehaviour : MonoBehaviour
{
    public int ToggledValue { get; set; } = 1;
    public int ToggledMode {get; set; } = 0;
    //[SerializeField] Spawner spawner;

    //Difficulty Toggles
    public void SetEasy()
    {
        //spawner.SetDifficulty(1);
        //Spawner.Instance.SetDifficulty(1);
        ToggledValue = 1;
        Difficulty.Instance.SetDifficulty(1);
    }
    public void SetMedium()
    {
        //spawner.SetDifficulty(2);
        //Spawner.Instance.SetDifficulty(2);
        ToggledValue = 2;
        Difficulty.Instance.SetDifficulty(2);
    }
    public void SetHard()
    {
        //spawner.SetDifficulty(3);
        //Spawner.Instance.SetDifficulty(3);
        ToggledValue = 3;
        Difficulty.Instance.SetDifficulty(3);
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
