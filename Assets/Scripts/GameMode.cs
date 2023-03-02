using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    public static GameMode Instance { get; private set; }
    [field: SerializeField] public int Gamemode { get; private set; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetGameMode(int mode)
    {
        Gamemode = mode;
    }
}
