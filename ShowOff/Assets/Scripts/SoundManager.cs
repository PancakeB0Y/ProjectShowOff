using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; // Added for AudioMixer support



public class SoundManager : MonoBehaviour
{

    public AudioMixer audioMixer;

    private AudioSource audioSource;
    private AudioSource loopAudioSource;
    private AudioSource backgroundMusicSource;

    [Header("Audio Mixer Groups")]
    public AudioMixerGroup sfxGroup;
    public AudioMixerGroup musicGroup;

    [Header("Top Priority Sounds")]
    [SerializeField] AudioClip backgroundMusic;
    [SerializeField] AudioClip menuMusic;

    [Header("Priority 2 Sounds")]
    [SerializeField] AudioClip doorOpen;
    [SerializeField] AudioClip doorClose;
    [SerializeField] AudioClip wind;
    [SerializeField] AudioClip lightMatch;

    public static SoundManager instance { get; private set; }

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

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        loopAudioSource = gameObject.AddComponent<AudioSource>();
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();

        // Assign AudioMixerGroups
        if (sfxGroup != null)
        {
            audioSource.outputAudioMixerGroup = sfxGroup;
            loopAudioSource.outputAudioMixerGroup = sfxGroup;
        }

        if (musicGroup != null)
        {
            backgroundMusicSource.outputAudioMixerGroup = musicGroup;
        }

        loopAudioSource.loop = true;
        backgroundMusicSource.clip = backgroundMusic;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.volume = 0.35f;
    }

    public void SetVolume(string parameter, float volume)
    {
        audioMixer.SetFloat(parameter, Mathf.Log10(volume) * 20); // Convert linear to logarithmic
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void PlayLoopingSound(AudioClip clip)
    {
        if (clip != null && loopAudioSource.clip != clip)
        {
            loopAudioSource.clip = clip;
            loopAudioSource.Play();
        }
    }

    public void StopLoopingSound()
    {
        if (loopAudioSource.isPlaying)
        {
            loopAudioSource.Stop();
            loopAudioSource.clip = null;
        }
    }

    // Top Priority Sounds
    public void PlayBackgroundMusic()
    {
        if (!backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Play();
        }
    }
    public void StopBackgroundMusic()
    {
        if (backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Stop();
        }
    }

    public void PlayMenuMusic()
    {
        PlayLoopingSound(menuMusic);
    }

    public void StopMenuMusic()
    {
        StopLoopingSound();
    }

    // Priority 2 Sounds

    public void PlayDoorOpenSound()
    {
        PlaySound(doorOpen);
    }

    public void PlayDoorCloseSound()
    {
        PlaySound(doorClose);
    }

    public void PlayWindSound()
    {
        PlaySound(wind);
    }

    public void PlayLightMatchSound()
    {
        PlaySound(lightMatch);
    }
}
