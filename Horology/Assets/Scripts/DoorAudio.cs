using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAudio : MonoBehaviour
{
    public AudioSource openDoorSound, closeDoorSound;
    private bool isClosed;
    // Start is called before the first frame update
    void Start()
    {
        isClosed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.doorsOpen && isClosed)
        {
            OpenDoorSound();
            isClosed = false;
        }
        else if (!GameManager.instance.doorsOpen && !isClosed)
        {
            CloseDoorSound();
            isClosed = true;
        }
    }

    private void FixedUpdate()
    {
        float audioSpeed = Mathf.Lerp(0.8f, 1f, TimeManager.instance.currentToBaseRatio);
        openDoorSound.pitch = audioSpeed;
        closeDoorSound.pitch = audioSpeed;

    }

    public void CloseDoorSound()
    {
        if (!closeDoorSound.isPlaying)
        {
            closeDoorSound.Play();
        }
    }

    public void OpenDoorSound()
    {
        if (!openDoorSound.isPlaying)
        {
            openDoorSound.Play();
        }
    }
}
