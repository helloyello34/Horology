using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomClipPlayer
{
    public List<AudioSource> clips;
    private List<bool> paused;

    public RandomClipPlayer(List<AudioSource> x)
    {
        clips = x;
    }

    public void PlaySound(int index)
    {
        if (index < clips.Count)
        {
            clips[index].Play();
        }
    }

    public void PlayRandomSound()
    {
        int clip = Random.Range(0, clips.Count);
        clips[clip].pitch = 1f;
        clips[clip].Play();
    }

    public bool isPlaying()
    {
        foreach (var clip in clips)
        {
            if (clip.isPlaying)
            {
                return true;
            }
        }
        return false;
    }

    public List<int> GetPlaying()
    {
        List<int> playing = new List<int>();
        for (var i = 0; i < clips.Count; i++)
        {
            if (clips[i].isPlaying)
            {
                playing.Add(i);
            }
        }
        return playing;
    }
}
