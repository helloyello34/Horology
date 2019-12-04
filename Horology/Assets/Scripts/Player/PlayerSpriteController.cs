using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpriteController : MonoBehaviour
{

    const int BACK = 0;
    const int FRONT = 1;
    const int LEFT_SIDE = 2;
    const int RIGHT_SIDE = 3;

    GameObject[] playerRotations = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        playerRotations[BACK] = transform.GetChild(BACK).gameObject;
        playerRotations[FRONT] = transform.GetChild(FRONT).gameObject;
        playerRotations[LEFT_SIDE] = transform.GetChild(LEFT_SIDE).gameObject;
        playerRotations[RIGHT_SIDE] = transform.GetChild(RIGHT_SIDE).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        float zRotation = PlayerManager.instance.gunTransform.rotation.z;

        Debug.Log("ZAxis: " + zRotation);

        if (zRotation >= -0.375 && zRotation <= 0.375)
        {
            playerRotations[BACK].SetActive(false);
            playerRotations[FRONT].SetActive(false);
            playerRotations[LEFT_SIDE].SetActive(false);
            playerRotations[RIGHT_SIDE].SetActive(true);
        }
        else if (zRotation > 0.375 && zRotation <= 0.875)
        {
            playerRotations[BACK].SetActive(true);
            playerRotations[FRONT].SetActive(false);
            playerRotations[LEFT_SIDE].SetActive(false);
            playerRotations[RIGHT_SIDE].SetActive(false);
        }
        else if ((zRotation > 0.875 && zRotation <= 1) || (zRotation <= -0.875 && zRotation >= -1))
        {
            playerRotations[BACK].SetActive(false);
            playerRotations[FRONT].SetActive(false);
            playerRotations[LEFT_SIDE].SetActive(true);
            playerRotations[RIGHT_SIDE].SetActive(false);
        }
        else
        {
            playerRotations[BACK].SetActive(false);
            playerRotations[FRONT].SetActive(true);
            playerRotations[LEFT_SIDE].SetActive(false);
            playerRotations[RIGHT_SIDE].SetActive(false);
        }
    }
}
