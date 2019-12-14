using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopups : MonoBehaviour
{
    public List<GameObject> popUps;
    private List<bool> isTriggered;
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
        Trigger(0, true);
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
                    Trigger(popUps.IndexOf(popup), false);
                }
            }
        }
    }

    public void Trigger(int index, bool open)
    {
        if(index > popUps.Count || index < 0)
        {
            return;
        }

        if (!isTriggered[index] || !open)
        {
            Time.timeScale = open ? 0f : 1f;
            Debug.Log(Time.timeScale);
            popUps[index].transform.parent.gameObject.SetActive(open);
            popUps[index].gameObject.SetActive(open);
            isTriggered[index] = true;
        }
    }
}
