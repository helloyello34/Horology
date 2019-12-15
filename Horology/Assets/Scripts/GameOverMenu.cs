using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    private bool isVisible;
    public GameObject canvas;
    public GameObject firstSelected;
    private GameObject currentSelected, lastSelected;
    public Color32 selectedColor, deselectedColor;
    public AudioSource navigationSound;

    private void Update()
    {
        if(EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(firstSelected);
        }
        if (currentSelected != EventSystem.current.currentSelectedGameObject)
        {
            lastSelected = currentSelected;
            currentSelected = EventSystem.current.currentSelectedGameObject;


            SetButtonColor(lastSelected, deselectedColor);
            ShowButtonIcon(lastSelected, false);

            SetButtonColor(currentSelected, selectedColor);
            ShowButtonIcon(currentSelected, true);

            navigationSound.Play();

        }
    }

    private void Awake()
    {
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }

    private void SetButtonColor(GameObject button, Color32 color)
    {
        if(button != null)
        {
            button.GetComponentInChildren<TextMeshProUGUI>(true).color = color;
        }
    }

    private void ShowButtonIcon(GameObject button, bool show)
    {
        if(button != null)
        {
            button.GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(show);
        }
    }

    public void ShowMenu(bool show)
    {
        GameManager.instance.isDead = show;
        isVisible = show;
        Time.timeScale = show ? 0f : 1f;
        canvas.SetActive(show);
        EventSystem.current.SetSelectedGameObject(firstSelected);
        currentSelected = firstSelected;

        SetButtonColor(currentSelected, selectedColor);
        ShowButtonIcon(currentSelected, true);
    }

    public void Restart()
    {
        ShowMenu(false);
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        ShowMenu(false);
        SceneManager.LoadScene(0);
    }
}
