using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class boolEvent : UnityEvent<bool>
{
}

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    private bool toggle;
    private bool isActive;
    private bool once = true;

    public UnityEvent<bool> timeEvent;

    public float timeFactor = 1;
    public float timeModifier;
    private float originalTimeFactor;
    public bool isSlowed = false;
    public bool isTimebarEmpty;

    private void Awake()
    {
        instance = this;
        originalTimeFactor = timeFactor;
        timeEvent = new boolEvent();
        toggle = PlayerPrefs.GetString("TimeJuiceMode", "Hold") == "Toggle";
    }

    private void InputHandler()
    {
        if (toggle)
        {
            if (Input.GetAxisRaw("Time") == 1 && once)
            {
                Debug.Log("here");
                isActive = !isActive;
                once = false;
            }
            else if (Input.GetAxisRaw("Time") == 0)
            {
                once = true;
            }
        }
        else
        {
            if (Input.GetAxisRaw("Time") == 1)
            {
                isActive = true;
            }
            else
            {
                isActive = false;
            }
        }
    }

    private void Update()
    {
        InputHandler();
        if (isActive && !isSlowed && !isTimebarEmpty)
        {
            timeFactor *= timeModifier;
            isSlowed = true;
            timeEvent.Invoke(true);
        }
        else if ((!isActive && isSlowed) || isTimebarEmpty)
        {
            timeFactor = originalTimeFactor;
            isSlowed = false;
            timeEvent.Invoke(false);
        }
    }
}
