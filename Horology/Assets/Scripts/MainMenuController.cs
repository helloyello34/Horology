using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string gameScene;
    public void PlayGame()
    {
        Debug.Log("PlayGame called");

        //Load Game scene
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame called");
        Application.Quit();
    }
}
