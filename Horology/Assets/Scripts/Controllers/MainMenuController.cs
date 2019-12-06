using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
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
        // This makes quit button also work in editor
        // UnityEditor.EditorApplication.isPlaying = false;
    }

    private void Update()
    {
        // When back is pressed on any other menu than the main menu
        if (Input.GetButtonDown("Cancel") && EventSystem.current.currentSelectedGameObject.transform.parent.name != "MainMenu")
        {
            // Back out of the menu
            // Only works if there's a button called precisely "Back button"
            GameObject.Find("Back button").GetComponent<Button>().onClick.Invoke();
        }
    }
}
