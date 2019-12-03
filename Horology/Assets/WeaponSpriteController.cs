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
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float zAxis = transform.rotation.z;

        SpriteRenderer armSpriteRenderer = ArmObject.GetComponent<SpriteRenderer>();
        if(zAxis < -0.7 || zAxis > 0.7)
        {
            //armTransform.position = new Vector3(transform.position.x + 0.08f, transform.position.y +0.07f, 0f);
            armSpriteRenderer.flipX = false;
            armSpriteRenderer.flipY = true;
            mySpriteRenderer.flipY = true;
        }
        else
        {
            //armTransform.position = new Vector3(transform.position.x + 0.135f,transform.position.y + (-0.082f), 0f);
            armSpriteRenderer.flipX = true;
            armSpriteRenderer.flipY = false;
            mySpriteRenderer.flipY = false;
        }

        if (zAxis < 0)
        {
            armSpriteRenderer.sortingOrder = 2;
            mySpriteRenderer.sortingOrder = 2;
        }
        else
        {
            armSpriteRenderer.sortingOrder = 0;
            mySpriteRenderer.sortingOrder = 0;
        }
    }
}
