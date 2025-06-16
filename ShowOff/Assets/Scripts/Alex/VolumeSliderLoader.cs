using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderLoader : MonoBehaviour
{
    public enum VolumeType { Master, Music, SFX }
    public VolumeType volumeType;

    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        switch (volumeType)
        {
            case VolumeType.Master:
                slider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
                break;
            case VolumeType.Music:
                slider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
                break;
            case VolumeType.SFX:
                slider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
                break;
        }
    }
}
