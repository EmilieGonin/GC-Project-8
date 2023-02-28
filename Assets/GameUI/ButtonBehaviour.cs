using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    public void OnStartClick()
    {
        //Debug.Log("Start button clicked");
        SceneManager.LoadScene("SampleScene");
    }
    public void OnQuitClick()
    {
        //Debug.Log("Quit button clicked");
        Application.Quit();
    }

    public void OnMenuClick()
    {
        //Debug.Log("Start button clicked");
        SceneManager.LoadScene("MenuScene");
    }
}
