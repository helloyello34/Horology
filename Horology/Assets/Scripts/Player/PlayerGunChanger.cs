using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunChanger : MonoBehaviour
{
    private int startingGun = -1;
    public List<GameObject> guns;
    private GameObject currentGun;
    // Start is called before the first frame update
    void Start()
    {
        string activeGun = gameObject.GetComponentInChildren<GunBase>(false).name;
        for (var i = 0; i < guns.Count; i++)
        {
            if (guns[i].name == activeGun)
            {
                startingGun = i;
                break;
            }
        }
        if (startingGun != -1)
        {
            currentGun = guns[startingGun];
        }
        else
        {
            Debug.Log("GUN NOT FOUND");
        }
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
