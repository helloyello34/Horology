using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public GameObject doorObject;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyManager.instance.FloorIsEmpty())
        {
            Debug.Log("Disabling the doors");
            for (int i = 0; i < doorObject.transform.childCount; i++)
            {
                if (doorObject.transform.GetChild(i).CompareTag("ClosedDoor"))
                {
                    doorObject.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
        else
        {
            Debug.Log("Closing The Doors");
            for (int i = 0; i < doorObject.transform.childCount; i++)
            {
                if (doorObject.transform.GetChild(i).CompareTag("ClosedDoor"))
                {
                    doorObject.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }
}
