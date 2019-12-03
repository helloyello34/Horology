using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int[] enemyCount;
    public int level;


    #region Singleton

    public static EnemyManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public bool FloorIsEmpty()
    {
        if (enemyCount[level] == 0)
        {
            level++;
            return true;
        }
        return false;
    }

    public void killEnemy()
    {
        enemyCount[level] -= 1;
    }
}
