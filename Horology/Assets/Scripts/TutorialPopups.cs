using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopups : MonoBehaviour
{
    public List<Canvas> popUps;
    private List<bool> isTriggered;
    // Start is called before the first frame update
    void Start()
    {
        isTriggered = new List<bool>();
        foreach (var _ in popUps)
        {
            isTriggered.Add(false);
        }
    }

    public void Trigger(int index)
    {
        if (popUps.Count > index && !isTriggered[index])
        {
            Time.timeScale = 0f;
            popUps[index].gameObject.SetActive(true);
            isTriggered[index] = true;
        }
    }
}
