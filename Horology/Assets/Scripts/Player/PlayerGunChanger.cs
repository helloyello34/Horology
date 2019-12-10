using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunChanger : MonoBehaviour
{
    public int startingGun;
    public List<GameObject> guns;
    private GameObject currentGun;
    // Start is called before the first frame update
    void Start()
    {
        currentGun = guns[startingGun];
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
