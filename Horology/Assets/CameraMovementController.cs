using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    
    // The object the camera follows
    public GameObject gameObject;

    // Update is called once per frame
    void Update()
    {
        // Follow 20% of the way to the player
        transform.position += (gameObject.transform.position - new Vector3(transform.position.x, transform.position.y, gameObject.transform.position.z)) * 0.1f;
    }
}
