using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; // Added for AudioMixer support



public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    // 2D Sounds that come from the sound manager

    [Header("BG Music")]
    [Space]

    [SerializeField]
    AudioSource audioSourceBG;
    [Space]

    [SerializeField] AudioClip bgMusic;


    [Header("SFX")]
    [Space]

    [SerializeField]
    AudioSource audioSourceSFX;
    [Space]

    // 3D Sounds that should be played from the object itself

    [Header("3D Object Sounds")]
    [Space]

    [SerializeField] AudioClip doorOpen;
    [SerializeField] AudioClip doorClose;
    [SerializeField] AudioClip wind;
    [SerializeField] AudioClip lightLantern;
    [SerializeField] AudioClip candleOn;
    [SerializeField] AudioClip candleOff;
    [SerializeField] AudioClip statueFollow;
    [SerializeField] AudioClip itemPickup;
    [SerializeField] AudioClip inventoryItemSelected;
    [SerializeField] AudioClip wrongItemChosen;
    [SerializeField] AudioClip statueSound;
    [SerializeField] AudioClip lockKeysClosing;


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(this);
        }
    }

    // 2D Sounds

    public void PlaySFXSound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSourceSFX.PlayOneShot(clip);
        }
    }

    // Top Priority Sounds
    //public void PlayBackgroundMusic()
    //{
    //    if (!backgroundMusicSource.isPlaying)
    //    {
    //        backgroundMusicSource.Play();
    //    }
    //}
    //public void StopBackgroundMusic()
    //{
    //    if (backgroundMusicSource.isPlaying)
    //    {
    //        backgroundMusicSource.Stop();
    //    }
    //}

    // 3D Sounds


    // Two variations of the method depending on if the sender already has a reference to the audio source
    private void Play3DSound(GameObject objectSoundIsComingFrom, AudioClip clip)
    {
        if (objectSoundIsComingFrom.TryGetComponent<AudioSource>(out AudioSource objectAudioSource))
        {
            Play3DSound(objectAudioSource, clip);
        }
    }

    private void Play3DSound(AudioSource audioSourceOfObject, AudioClip clip)
    {
        audioSourceOfObject.PlayOneShot(clip);
    }

    public void PlayWindSound(GameObject objectSoundIsComingFrom)
    {
        Play3DSound(objectSoundIsComingFrom, wind);
    }

    public void PlayLightLanternSound(GameObject objectSoundIsComingFrom)
    {
        Play3DSound(objectSoundIsComingFrom, lightLantern);
    }

    public void PlayCandleOnSound(GameObject objectSoundIsComingFrom)
    {
        Play3DSound(objectSoundIsComingFrom, candleOn);
    }

    public void PlayCandleOffSound(GameObject objectSoundIsComingFrom)
    {
        Play3DSound(objectSoundIsComingFrom, candleOff);
    }

    public void PlayItemPickupSound(GameObject objectSoundIsComingFrom)
    {
        Play3DSound(objectSoundIsComingFrom, itemPickup);
    }

    public void PlayInventoryItemSelectedSound(GameObject objectSoundIsComingFrom)
    {
        Play3DSound(objectSoundIsComingFrom, inventoryItemSelected);
    }

    public void PlayWrongItemChosenSound(GameObject objectSoundIsComingFrom)
    {
        Play3DSound(objectSoundIsComingFrom, wrongItemChosen);
    }

    public void PlayStatueSound(GameObject objectSoundIsComingFrom)
    {
        Play3DSound(objectSoundIsComingFrom, statueSound);
    } 

    public void PlayLockKeysClosingSound(GameObject objectSoundIsComingFrom)
    {
        Play3DSound(objectSoundIsComingFrom, lockKeysClosing);
    }
}
