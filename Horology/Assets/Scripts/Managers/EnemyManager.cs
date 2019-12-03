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
        if (enemyCount[GameManager.instance.currentLevel] == 0)
        {
            return true;
        }
        return false;
    }

    public void killEnemy()
    {
        enemyCount[GameManager.instance.currentLevel] -= 1;
    }
}
