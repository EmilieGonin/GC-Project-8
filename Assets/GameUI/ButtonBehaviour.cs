using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void OnQuitClick()
    {
        Application.Quit();
    }

    public void OnMenuClick()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void OnNewGameClick()
    {
        SceneManager.LoadScene("NewGameScene");
    }
}
