using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public int bossRoom, nextLevelBuildIndex;
    public GameObject stairs;
    public GameObject winScreen;
    private int roomCount;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name != "Player" || !stairs.activeInHierarchy)
        {
            return;
        }

        if(nextLevelBuildIndex != 0)
        {
            SceneManager.LoadScene(nextLevelBuildIndex);

        }
        else
        {

            winScreen.SetActive(true);
            GameManager.instance.isWin = true;
            Time.timeScale = 0f;
            //show win screen
        }
    }

     

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isPaused)
        {
            Time.timeScale = 0f;
        }
        roomCount = EnemyManager.instance.enemyCount[bossRoom];    
        
        if(EnemyManager.instance.enemyCount[bossRoom] < 1 && !stairs.activeInHierarchy)
        {
            stairs.SetActive(true);
        }
    }
}
