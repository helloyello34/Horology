using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircleSpreadGun : EnemyWeapon
{
    public int bulletAmount = 12;

    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Shoot()
    {

        // Amount angle changes each iteration
        float angleMultiplier = 360f / (float)bulletAmount;

        //Iteration for each bullet
        for (int i = 0; i < bulletAmount; i++)
        {
            //Change degrees to radians
            var radians = (i * angleMultiplier) * Mathf.Deg2Rad;
            //Find the position of the bullet
            Vector2 bulletPosition = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) + (Vector2)transform.position;

            //Change rotation of enemy to point to bullet, bullet will travel along this path
            transform.rotation = Quaternion.Euler(0f, 0f, i * angleMultiplier);
            //Instatiate the bullet
            Instantiate(bulletPrefab, bulletPosition, transform.rotation);
        }
    }
}
