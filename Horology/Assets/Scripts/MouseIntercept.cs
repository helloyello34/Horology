﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseIntercept : MonoBehaviour
{
    private GameObject currentObject;
    public GameObject fallback;
    // Start is called before the first frame update
    void Start()
    {
        currentObject = EventSystem.current.currentSelectedGameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (currentObject == null)
            {
            }
            EventSystem.current.SetSelectedGameObject(currentObject);
        }
        currentObject = EventSystem.current.currentSelectedGameObject;
    }
}
