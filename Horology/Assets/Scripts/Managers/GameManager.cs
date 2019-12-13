using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        doorsOpen = true;
    }

    #endregion

    public int currentLevel;
    public bool doorsOpen;

    public void EnterLeve(int level)
    {
        currentLevel = level;
    }
}
