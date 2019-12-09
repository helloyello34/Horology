using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public GameObject doorObject;


    // Update is called once per frame
    void Update()
    {
        if (EnemyManager.instance.FloorIsEmpty())
        {
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
