using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    private bool isVisible;
    private GameObject firstSelected;
    public GameObject menuCanvas, firstMenu, otherButton;
    // private GameObject currentSelected;
    // Start is called before the first frame update
    void Start()
    {
        isVisible = false;
        EventSystem.current.SetSelectedGameObject(otherButton);
        // Button that will be highlighted on menu open
        firstSelected = EventSystem.current.firstSelectedGameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            ShowMenu(!isVisible);
            Debug.Log(menuCanvas.name);
        }

        if (isVisible && Input.GetButtonDown("Cancel"))
        {
            if (firstMenu.activeInHierarchy)
            {
                ShowMenu(!isVisible);
            }
            else
            {
                GameObject.Find("Back button").GetComponent<Button>().onClick.Invoke();
            }
        }

    }

    private void ShowMenu(bool show)
    {
        // Fixes issue where buttons are not loaded in Start() and other is still null
        if (otherButton == null)
        {
            otherButton = GameObject.Find("Quit button");
            Debug.Log("other btn: " + otherButton.name);
            // Makes sure the resume button will still be correctly higlighted if issue occurs
            if (!isVisible)
            {
                EventSystem.current.SetSelectedGameObject(otherButton);
            }
        }
        // Pause and unpause toggle
        Debug.Log("Menu toggled");
        // Toggle menu visibility flag 
        isVisible = !isVisible;
        // Stop or start time
        Time.timeScale = isVisible ? 0f : 1f;
        // Activate or deactivate the menu canvas
        menuCanvas.SetActive(isVisible);
        // Set the resume button on menu open, other button on menu close
        // Setting to another on close makes sure the resume button is correctly highlighted if menu is closed
        // while resume is highlighted
        EventSystem.current.SetSelectedGameObject(isVisible ? firstSelected : otherButton);
    }

    public void Resume()
    {
        Debug.Log("Resumed");
        ShowMenu(false);
    }

    public void Restart()
    {
        Debug.Log("Restart");
        ShowMenu(false);
        // Reload the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Debug.Log("Quit to menu");
        ShowMenu(false);
        // Load main menu
        SceneManager.LoadScene(0);
    }
}
