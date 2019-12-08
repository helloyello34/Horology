using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimMechanic : MonoBehaviour
{

    private Vector3 looking = new Vector3();

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        // Get the R axis on the joystick
        looking.x = Input.GetAxis("ShootHorizontal");
        looking.y = Input.GetAxis("ShootVertical");
        // If the button is not in the deadzone change the rotation
        if (!(looking.x <= 0.2 && looking.x >= -0.2 && looking.y <= 0.2 && looking.y >= -0.2))
        {
            PlayerManager.instance.gunTransform = transform;
            // Get the angle of the joystick and rotating the object on that angle
            float angle = Mathf.Atan2(looking.y, looking.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, -angle);
        }
    }
}
