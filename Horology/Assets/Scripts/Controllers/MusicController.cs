using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicController : MonoBehaviour
{
    public List<AudioSource> songs;
    public List<bool> paused;
    public float minPitch;
    private float currentPitch, fadeTime;
    private RandomClipPlayer clipPlayer;
    private UnityAction<bool> timeAction;
    private bool isSlowed;

    // Start is called before the first frame update
    void Start()
    {
        clipPlayer = new RandomClipPlayer(songs);
        paused = new List<bool>();
        foreach (var item in songs)
        {
            paused.Add(false);
        }
        timeAction += TimeCallback;
        TimeManager.instance.timeEvent.AddListener(timeAction);
        currentPitch = 1f;
        fadeTime = TimeManager.instance.timeIncrementer;
        PlaySong();
    }

    public void PlaySong()
    {
        clipPlayer.PlayRandomSound();
    }

    public void ChangePitch(float delta)
    {
        currentPitch = Mathf.Lerp(minPitch, 1f, TimeManager.instance.currentToBaseRatio);
    }

    private void Update()
    {
        foreach (var item in clipPlayer.GetPlaying())
        {
            songs[item].pitch = GameManager.instance.isPaused ? 0f : currentPitch;
        }
    }

    private void FixedUpdate()
    {
        // Debug.Log();
        ChangePitch(isSlowed ? -fadeTime * Time.fixedDeltaTime : fadeTime * Time.fixedDeltaTime);
        if (currentPitch > minPitch && currentPitch < 1f)
        {
            foreach (var item in clipPlayer.GetPlaying())
            {
                paused[item] = false;
                songs[item].pitch = currentPitch;
            }
        }
    }

    private void TimeCallback(bool isSlow)
    {
        isSlowed = isSlow;
    }
}
