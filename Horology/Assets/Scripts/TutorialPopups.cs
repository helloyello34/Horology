using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPopups : MonoBehaviour
{
    public List<GameObject> popUps;
    private List<bool> isTriggered;
    private bool open;
    // Start is called before the first frame update
    void Awake()
    {
        isTriggered = new List<bool>();
        foreach (var _ in popUps)
        {
            isTriggered.Add(false);
        }
    }

    private void Start()
    {
        SetOpen(true);
        Trigger(0);
    }

    private void Update()
    {
        foreach(var popup in popUps)
        {
            if(popup.activeInHierarchy)
            {
                PlayerManager.instance.player.GetComponent<Player>().isGod = true;
                if(Input.GetButtonDown("Submit") && !GameManager.instance.isPaused)
                {
                    //SetOpen(false);
                    PlayerManager.instance.player.GetComponent<Player>().isGod = false;
                    Trigger(popUps.IndexOf(popup));

                    if(popUps.IndexOf(popup) == popUps.Count - 1)
                    {
                        Time.timeScale = 1f;
                        SceneManager.LoadScene(0);
                    }
                }

            }
        }
    }

    public void SetOpen(bool open)
    {
        this.open = open;
        //Time.timeScale = open ? 0f : 1f;
    }


    public void Trigger(int index)
    {
        if(index > popUps.Count || index < 0)
        {
            return;
        }

        if (!isTriggered[index] && open)
        {
            Time.timeScale = 0f;
            open = false;
            popUps[index].transform.parent.gameObject.SetActive(true);
            popUps[index].gameObject.SetActive(true);
            isTriggered[index] = true;
            GameManager.instance.isDead = true;

        }
        else if(!open)
        {
            Time.timeScale = 1f;
            popUps[index].transform.parent.gameObject.SetActive(false);
            popUps[index].gameObject.SetActive(false);
            GameManager.instance.isDead = false;
        }
    }
}
