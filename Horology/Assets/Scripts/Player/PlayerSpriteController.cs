using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteController
    : MonoBehaviour
{
    public SpriteRenderer[] renderers;

    public GameObject pivot;

    [Header("Damage Color Effect")]
    public Color regularColor;
    public Color hitColor;

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

    public void HitEffect()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].color = hitColor;
        }
    }

    public void RegularSpriteColor()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].color = regularColor;
        }
    }
}
