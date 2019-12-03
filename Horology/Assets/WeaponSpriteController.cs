using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSpriteController : MonoBehaviour
{
    

    SpriteRenderer mySpriteRenderer;
    public GameObject ArmObject;

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
        SpriteRenderer armSpriteRenderer = ArmObject.GetComponent<SpriteRenderer>();
        if(zAxis < -0.7 || zAxis > 0.7)
        {
            // Flip the Axis so the player is looking to the left
            armSpriteRenderer.flipX = false;
            armSpriteRenderer.flipY = true;
            mySpriteRenderer.flipY = true;
        }
        else
        {
            // Flip the Axis so the player is looking to the right
            armSpriteRenderer.flipX = true;
            armSpriteRenderer.flipY = false;
            mySpriteRenderer.flipY = false;
        }

        if (zAxis < 0)
        {
            // If the player is looking towards the screen
            armSpriteRenderer.sortingOrder = 2;
            mySpriteRenderer.sortingOrder = 2;
        }
        else
        {
            // If the player is looking forward from the screen
            armSpriteRenderer.sortingOrder = 0;
            mySpriteRenderer.sortingOrder = 0;
        }
    }
}
