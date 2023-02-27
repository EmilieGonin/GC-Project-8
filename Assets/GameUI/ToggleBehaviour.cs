using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBehaviour : MonoBehaviour
{
    [SerializeField] Spawner spawner;
    public void SetEasy()
    {
        spawner.SetDifficulty(1);
        Debug.Log("1");
    }
    public void SetMedium()
    {
        spawner.SetDifficulty(2);
        Debug.Log("2");
    }
    public void SetHard()
    {
        spawner.SetDifficulty(3);
        Debug.Log("3");
    }
}
