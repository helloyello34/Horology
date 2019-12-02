using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    private bool isVisible;
    private GameObject empty, firstSelected;
    public GameObject menuCanvas, firstMenu;
    // private GameObject currentSelected;
    // Start is called before the first frame update
    void Start()
    {
        isVisible = false;
        empty = new GameObject();
        firstSelected = EventSystem.current.firstSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(empty);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause") || (isVisible && Input.GetButtonDown("Cancel")))
        {
            Debug.Log(transform.name);
            MenuToggle();
        }
    }

    private void MenuToggle()
    {
        // Pause and unpause toggle
        Debug.Log("Menu toggled");
        isVisible = !isVisible;
        Time.timeScale = isVisible ? 0f : 1f;
        menuCanvas.SetActive(isVisible);
        EventSystem.current.SetSelectedGameObject(isVisible ? firstSelected : empty);
    }

    public void Resume()
    {
        Debug.Log("Resumed");
        MenuToggle();
    }

    public void Restart()
    {
        Debug.Log("Restart");
        MenuToggle();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Debug.Log("Quit to menu");
        MenuToggle();
        SceneManager.LoadScene(0);
    }
}
