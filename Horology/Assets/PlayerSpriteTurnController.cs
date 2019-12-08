using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteTurnController : MonoBehaviour
{
    public SpriteRenderer[] renderers;

    public GameObject pivot;

    // Update is called once per frame
    void Update()
    {
        bool flip = true;
        if(pivot.transform.rotation.z <= 0.7f && pivot.transform.rotation.z >= -0.7f)
        {
            flip = false;
        }

        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.flipX = flip;
        }
    }
}
