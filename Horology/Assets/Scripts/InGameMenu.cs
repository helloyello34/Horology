using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    private bool isVisible;
    private GameObject firstSelected;
    public GameObject menuCanvas, firstMenu, otherButton;
    public Color32 selectedColor;
    public Color32 deselectedColor;
    private GameObject currentlySelected, lastSelected;
    public TextMeshProUGUI timeText, shootText;
    // Start is called before the first frame update
    void Start()
    {
        isVisible = false;
        Time.timeScale = 1f;
        EventSystem.current.SetSelectedGameObject(otherButton);
        // Button that will be highlighted on menu open
        firstSelected = EventSystem.current.firstSelectedGameObject;
        currentlySelected = firstSelected;
        SetButtonColor(firstSelected, selectedColor);
        ShowButtonIcon(firstSelected, true);

        timeText.text = PlayerPrefs.GetString("TimeJuiceMode", "Hold");
        shootText.text = PlayerPrefs.GetString("ShootingMode", "Manual");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (isVisible && EventSystem.current.currentSelectedGameObject.transform.parent.name != "MainMenu")
            {
                GameObject.Find("Back button").GetComponent<Button>().onClick.Invoke();
            }
            ShowMenu(!isVisible);
        }

        if (isVisible)
        {
            if (EventSystem.current.currentSelectedGameObject != currentlySelected)
            {
                lastSelected = currentlySelected;
                currentlySelected = EventSystem.current.currentSelectedGameObject;

                SetButtonColor(lastSelected, deselectedColor);
                ShowButtonIcon(lastSelected, false);

                SetButtonColor(currentlySelected, selectedColor);
                ShowButtonIcon(currentlySelected, true);
            }

            if (EventSystem.current.currentSelectedGameObject.transform.parent.name == "Settings menu" && EventSystem.current.currentSelectedGameObject.name != "Back button")
            {
                GameObject selected = EventSystem.current.currentSelectedGameObject;
                TextMeshProUGUI selectedText = selected.GetComponentsInChildren<TextMeshProUGUI>(true)[1];
                if (selected.name == "TimeJuiceMode")
                {
                    if (Input.GetButtonDown("Submit"))
                    {
                        PlayerPrefs.SetString("TimeJuiceMode", selectedText.text == "Hold" ? "Toggle" : "Hold");
                        selectedText.text = PlayerPrefs.GetString("TimeJuiceMode", "Auto");
                    }
                }
                else if (selected.name == "ShootingMode")
                {
                    if (Input.GetButtonDown("Submit"))
                    {
                        PlayerPrefs.SetString("ShootingMode", selectedText.text == "Manual" ? "Auto" : "Manual");
                        selectedText.text = PlayerPrefs.GetString("ShootingMode", "Manual");
                    }
                }
            }

            if (Input.GetButtonDown("Cancel"))
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

    }

    private void SetButtonColor(GameObject button, Color32 color)
    {
        button.GetComponentInChildren<TextMeshProUGUI>(true).color = color;
    }

    private void ShowButtonIcon(GameObject button, bool show)
    {
        button.GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(show);
    }

    private void ShowMenu(bool show)
    {
        // Fixes issue where buttons are not loaded in Start() and other is still null
        if (otherButton == null)
        {
            otherButton = GameObject.Find("Quit button");
            // Makes sure the resume button will still be correctly higlighted if issue occurs
            if (!isVisible)
            {
                EventSystem.current.SetSelectedGameObject(otherButton);
            }
        }
        // Pause and unpause toggle
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
        ShowMenu(false);
    }

    public void Restart()
    {
        ShowMenu(false);
        // Reload the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        ShowMenu(false);
        // Load main menu
        SceneManager.LoadScene(0);
    }
}
