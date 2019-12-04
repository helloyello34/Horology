using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLoot : MonoBehaviour
{
    //Most frequent to less frequent
    public int[] table = { 
        55, // Time Juice
        35, // Health
        10  // Upgrade
    };

    public List<GameObject> loots;

    int total;
    int random;
    int chanceToEnterTable = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Calculate total
        foreach(var item in table)
        {
            total += item;
        }
    }

    public GameObject getRandomLoot()
    {

        if(Random.Range(1, chanceToEnterTable + 1) == chanceToEnterTable)
        {
            // Random a number from 0 to total + 1(because exclusive)
            random = Random.Range(0, total + 1);

            // Loop through each index of the table array
            for(int i = 0; i < table.Length; i++)
            {
                // If random number is less or equal to table number, choose that item
                if(random <= table[i])
                {
                    Debug.Log("Returning gameobject");
                    return loots[i];
                }
                else // If not substract random and loop again
                {
                    random -= table[i];
                }
            }
        }
        return null;
    }
}
