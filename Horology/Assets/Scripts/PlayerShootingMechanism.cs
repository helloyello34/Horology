using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingMechanism : MonoBehaviour
{
    GameObject firePosition;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.GetAxis("ShootHorizontal") + " == " + Input.GetAxis("ShootVertical"));
    }
}
