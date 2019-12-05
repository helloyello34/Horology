using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int[] enemyCount;


    #region Singleton

    public static EnemyManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public bool FloorIsEmpty()
    {
        if (enemyCount[GameManager.instance.currentLevel] <= 0)
        {
            return true;
        }
        return false;
    }


    // Kills enemy on the lvl their on
    public void killEnemy(int lvl)
    {
        enemyCount[lvl] -= 1;
    }
}
