using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSpriteController : MonoBehaviour
{
    

    SpriteRenderer mySpriteRenderer;

    private void Start()
    {
        // Get the Sprite Renderer from the gameobject
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the z rotation on the object
        float zAxis = transform.rotation.z;

        // Get the sprite renderer from the arm game object
        if(zAxis < -0.7 || zAxis > 0.7)
        {
            // Flip the Axis so the player is looking to the left
            mySpriteRenderer.flipY = true;
        }
        else
        {
            // Flip the Axis so the player is looking to the right
            mySpriteRenderer.flipY = false;
        }
    }
}
