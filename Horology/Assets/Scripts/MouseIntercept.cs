using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseIntercept : MonoBehaviour
{
    private GameObject currentObject;
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
                Debug.Log("non");
            }
            EventSystem.current.SetSelectedGameObject(currentObject);
        }
        currentObject = EventSystem.current.currentSelectedGameObject;
    }
}
