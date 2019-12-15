using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    private GameObject currentButton;
    private GameObject lastButton;
    public Color32 selectedColor;
    public Color32 deselectedColor;
    private string shootingMode;
    private string timeJuiceMode;
    public AudioSource navigationSound;

    private void Start()
    {
        currentButton = EventSystem.current.firstSelectedGameObject;
        lastButton = currentButton;
        SetTextColour(currentButton, selectedColor);
        SetButtonIcon(currentButton, true);

        foreach (var text in gameObject.GetComponentsInChildren<TextMeshProUGUI>(true))
        {
            if (text.name == "Setting")
            {
                text.text = PlayerPrefs.GetString(text.transform.parent.name, text.text);
            }
        }
    }
    public void PlayGame()
    {
        //Load Game scene
        SceneManager.LoadScene(2);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();

        // This makes quit button also work in editor. Needs to be commented for build
        // UnityEditor.EditorApplication.isPlaying = false;
    }

    private void SetTextColour(GameObject button, Color32 color)
    {
        button.GetComponentInChildren<TextMeshProUGUI>().color = color;
    }

    private void SetButtonIcon(GameObject button, bool isSet)
    {
        button.GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(isSet);
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

        if (EventSystem.current.currentSelectedGameObject != currentButton)
        {
            lastButton = currentButton;
            currentButton = EventSystem.current.currentSelectedGameObject;

            SetTextColour(currentButton, selectedColor);
            SetButtonIcon(currentButton, true);
            SetTextColour(lastButton, deselectedColor);
            SetButtonIcon(lastButton, false);

            navigationSound.Play();
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
    }
}
