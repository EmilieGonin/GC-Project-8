using UnityEngine;

public class AmbientSound : MonoBehaviour
{
    public static AmbientSound Instance { get; private set; }
    [SerializeField] AudioSource _ambientMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _ambientMusic.Play();
            DontDestroyOnLoad(gameObject);
        }
    }
}
