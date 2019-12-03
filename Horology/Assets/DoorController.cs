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
            doorObject.transform.GetChild(1).gameObject.SetActive(false);
            //Destroy(doorObject.transform.GetChild(1).gameObject);
        }
    }
}
