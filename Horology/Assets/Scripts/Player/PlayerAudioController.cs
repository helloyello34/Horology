using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public List<AudioSource> hurtSounds;

    [HideInInspector]
    public RandomClipPlayer clipPlayer;

    private void Start()
    {
        clipPlayer = new RandomClipPlayer(hurtSounds);
    }


    public void PlayHurt()
    {
        if(!GameManager.instance.isDead || GameManager.instance.isWin)
        {
            clipPlayer.PlayRandomSound();
        }
    }
}
