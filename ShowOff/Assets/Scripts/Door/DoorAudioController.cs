using UnityEngine;

public class DoorAudioController : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip doorOpen;
    [SerializeField] private AudioClip doorClose;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDoorOpenSound()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();

        audioSource.clip = doorOpen;
        audioSource.Play();
    }

    public void PlayDoorCloseSound()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();

        audioSource.clip = doorClose;
        audioSource.Play();
    }
}