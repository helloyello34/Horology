using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    
    // The object the camera follows
    public GameObject myGameObject;

    // Update is called once per frame
    void Update()
    {
        // Follow 20% of the way to the player
        transform.position += (myGameObject.transform.position - new Vector3(transform.position.x, transform.position.y, myGameObject.transform.position.z)) * 0.1f;
    }
}
