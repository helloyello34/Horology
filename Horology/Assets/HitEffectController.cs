using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectController : MonoBehaviour
{
    public AudioSource hitSound;

    void Start()
    {
        Destroy(gameObject, 0.2f);
    }
}