using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    [SerializeField] GameObject pauseCanvas;

    public void Start()
    {
        pauseCanvas.SetActive(false);
    }
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

    public void OnPauseClick() // !!!
    {
        //Canvas pause = GameObject.Find("PauseCanvas").GetComponent<Canvas>();
        Debug.Log("Pause Button clicked");
        if (Time.timeScale == 1) 
        { 
            Time.timeScale = 0;
            //Instantiate(pauseCanvas); 
            pauseCanvas.SetActive(true);
        } 
        else 
        { 
            Time.timeScale = 1;
            pauseCanvas.SetActive(false);
        }
        
        
    }
    public void OnMenuClick()
    {
        //Debug.Log("Start button clicked");
        SceneManager.LoadScene("MenuScene");
    }
    public void OnCloseClick() // !!!
    {
        Debug.Log("Close Button clicked");
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
    }
}
