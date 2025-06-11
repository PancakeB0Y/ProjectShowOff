using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FMODVolumeController : MonoBehaviour
{
    FMOD.Studio.Bus musicBus;
    FMOD.Studio.Bus sfxBus;

    void Start()
    {
        musicBus = RuntimeManager.GetBus("bus:/Music");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
    }

    public void SetMusicVolume(float volume)
    {
        musicBus.setVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxBus.setVolume(volume);
    }
}
