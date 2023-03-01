using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public static Difficulty Instance { get; private set; }
    [field: SerializeField] public int GameDifficulty { get; private set; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetDifficulty(int difficulty)
    {
        GameDifficulty = difficulty;
    }
}
