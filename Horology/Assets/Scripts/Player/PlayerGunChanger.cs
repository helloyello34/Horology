using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunChanger : MonoBehaviour
{
    public List<GameObject> guns;
    public GameObject currentGun;
    // Start is called before the first frame update
    void Start()
    {
        currentGun = guns[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetActiveGun(int gunIndex)
    {
        if (guns.Count > gunIndex)
        {
            currentGun.SetActive(false);
            guns[gunIndex].SetActive(true);
            currentGun = guns[gunIndex];
        }
    }
}
