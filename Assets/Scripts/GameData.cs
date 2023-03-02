using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }
    [field: SerializeField] public int GameDifficulty { get; private set; }
    [field: SerializeField] public int GameMode { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetDifficulty(int difficulty)
    {
        GameDifficulty = difficulty;
    }

    public void SetGameMode(int mode)
    {
        GameMode = mode;
    }
}
