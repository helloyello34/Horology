using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : MonoBehaviour
{
    public int weaponIndex;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player)
        {
            PlayerManager.instance.player.GetComponentInChildren<PlayerGunChanger>().SetActiveGun(weaponIndex);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
