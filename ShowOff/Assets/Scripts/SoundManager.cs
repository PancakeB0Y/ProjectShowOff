using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    [Header("2D Sounds (UI, Global SFX, etc.)")]
    [SerializeField] EventReference bgMusicEvent;
    [SerializeField] EventReference inventoryItemSelectedEvent;
    [SerializeField] EventReference wrongItemChosenEvent;

    [Header("3D Object Sounds")]
    [SerializeField] EventReference doorOpenEvent;
    [SerializeField] EventReference doorCloseEvent;
    [SerializeField] EventReference windEvent;
    [SerializeField] EventReference lightLanternEvent;
    [SerializeField] EventReference candleOnEvent;
    [SerializeField] EventReference candleOffEvent;
    [SerializeField] EventReference statueFollowEvent;
    [SerializeField] EventReference itemPickupEvent;
    [SerializeField] EventReference statueSoundEvent;
    [SerializeField] EventReference lockKeysClosingEvent;
    [SerializeField] EventReference playerWalkEvent;

    private EventInstance bgMusicInstance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        // Uncomment if you want the sound manager to persist across scenes:
        // DontDestroyOnLoad(this.gameObject);
    }

    // ─────────────────────────────────────────────────────────────
    // 2D SOUNDS (One-shot, fire and forget)
    // ─────────────────────────────────────────────────────────────
    public void PlaySFX2D(EventReference eventRef)
    {
        if (eventRef.IsNull) return;
        RuntimeManager.PlayOneShot(eventRef);

    }
    void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayInventoryItemSelected()
    {
        PlaySFX2D(inventoryItemSelectedEvent);
    }

    public void PlayWrongItemChosen()
    {
        PlaySFX2D(wrongItemChosenEvent);
    }

    // ─────────────────────────────────────────────────────────────
    // BACKGROUND MUSIC (Persistent, looped event instance)
    // ─────────────────────────────────────────────────────────────
    public void PlayBackgroundMusic()
    {
        if (bgMusicInstance.isValid())
        {
            bgMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            bgMusicInstance.release();
        }

        bgMusicInstance = RuntimeManager.CreateInstance(bgMusicEvent);
        bgMusicInstance.start();
    }

    public void StopBackgroundMusic()
    {
        if (bgMusicInstance.isValid())
        {
            bgMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            bgMusicInstance.release();
        }
    }

    private void OnDestroy()
    {
        StopBackgroundMusic();
    }

    // ─────────────────────────────────────────────────────────────
    // 3D SOUNDS (Play at position of source GameObject)
    // ─────────────────────────────────────────────────────────────
    public void Play3DSound(EventReference eventRef, GameObject source)
    {
        if (eventRef.IsNull || source == null) return;
        RuntimeManager.PlayOneShot(eventRef, source.transform.position);
    }

    public void PlayDoorOpenSound(GameObject source)
    {
        Play3DSound(doorOpenEvent, source);
    }

    public void PlayDoorCloseSound(GameObject source)
    {
        Play3DSound(doorCloseEvent, source);
    }

    public void PlayWindSound(GameObject source)
    {
        Play3DSound(windEvent, source);
    }

    public void PlayLightLanternSound(GameObject source)
    {
        Play3DSound(lightLanternEvent, source);
    }

    public void PlayCandleOnSound(GameObject source)
    {
        Play3DSound(candleOnEvent, source);
    }

    public void PlayCandleOffSound(GameObject source)
    {
        Play3DSound(candleOffEvent, source);
    }

    public void PlayStatueFollowSound(GameObject source)
    {
        Play3DSound(statueFollowEvent, source);
    }

    public void PlayItemPickupSound(GameObject source)
    {
        Play3DSound(itemPickupEvent, source);
    }

    public void PlayStatueSound(GameObject source)
    {
        Play3DSound(statueSoundEvent, source);
    }

    public void PlayLockKeysClosingSound(GameObject source)
    {
        Play3DSound(lockKeysClosingEvent, source);
    }

    public void PlayPlayerWalkSound(GameObject source)
    {
        Play3DSound(playerWalkEvent, source);
    }
}
