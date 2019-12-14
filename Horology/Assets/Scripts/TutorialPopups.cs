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
                Time.timeScale = 0f;
                if(Input.GetButtonDown("Submit"))
                {
                    SetOpen(false);
                    Trigger(popUps.IndexOf(popup));

                    if(popUps.IndexOf(popup) == popUps.Count - 1)
                    {
                        SceneManager.LoadScene(0);
                    }
                }
            }
        }
    }

    public void SetOpen(bool open)
    {
        this.open = open;
    }


    public void Trigger(int index)
    {
        if(index > popUps.Count || index < 0)
        {
            return;
        }

        if (!isTriggered[index] || !open)
        {
            Time.timeScale = open ? 0f : 1f;
            popUps[index].transform.parent.gameObject.SetActive(open);
            popUps[index].gameObject.SetActive(open);
            isTriggered[index] = true;
        }
    }
}
