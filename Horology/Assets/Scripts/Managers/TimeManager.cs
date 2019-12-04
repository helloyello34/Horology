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
    private void Awake()
    {
        instance = this;
        originalTimeFactor = timeFactor;
        timeEvent = new boolEvent();
    }

    public UnityEvent<bool> timeEvent;

    public float timeFactor = 1;
    public float timeModifier;
    private float originalTimeFactor;
    public bool isSlowed = false;
    public bool isTimebarEmpty;

    private void Update()
    {
        if (Input.GetAxisRaw("Time") == 1 && !isSlowed && !isTimebarEmpty)
        {
            // Debug.Log("time slowed: " + (timeFactor - timeModifier));
            timeFactor *= timeModifier;
            isSlowed = true;
            timeEvent.Invoke(true);
        }
        else if ((Input.GetAxisRaw("Time") == 0 && isSlowed) || isTimebarEmpty)
        {
            // Debug.Log("time back to normal: " + (timeFactor + timeModifier));
            timeFactor = originalTimeFactor;
            isSlowed = false;
            timeEvent.Invoke(false);
        }
    }
}
