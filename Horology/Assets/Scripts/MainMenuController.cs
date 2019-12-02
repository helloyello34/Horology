﻿using System.Collections;
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
        UnityEditor.EditorApplication.isPlaying = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && EventSystem.current.currentSelectedGameObject.transform.parent.name != "MainMenu")
        {
            GameObject.Find("Back button").GetComponent<Button>().onClick.Invoke();
        }
    }
}
