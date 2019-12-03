using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    private bool isVisible;
    public GameObject canvas;
    public GameObject firstSelected;

    public void ShowMenu(bool show)
    {
        isVisible = show;
        Time.timeScale = show ? 0f : 1f;
        canvas.SetActive(show);
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }

    public void Restart()
    {
        ShowMenu(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        ShowMenu(false);
        SceneManager.LoadScene(0);
    }
}
