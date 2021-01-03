using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource BGMusic;
    public AudioSource SFX;


    

    private void Awake()
    {
        

        DontDestroyOnLoad(gameObject);

        if (GameObject.FindGameObjectsWithTag("SoundManager").Length>1)
        {
            Destroy(gameObject);
        }
    }

    public void ToggleMute()
    {
        BGMusic.mute = !BGMusic.mute;
        SFX.mute = !SFX.mute;
    }

    public void ToggleVolume(float vol)
    {
        BGMusic.volume = vol;
        SFX.volume = vol;
    }

    public void PlaySoundEffect(AudioClip sound)
    {
        SFX.PlayOneShot(sound);
    }
}
