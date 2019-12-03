using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpriteController : MonoBehaviour
{
    public Sprite playerBack;
    public Sprite playerFront;
    public GameObject rotationChild;

    SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (rotationChild.transform.rotation.z < 0)
        {
            spriteRenderer.sprite = playerFront;
        }
        else
        {
            spriteRenderer.sprite = playerBack;
        }
    }
}
