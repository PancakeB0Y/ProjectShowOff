using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class DoorAudioController : MonoBehaviour
{
    [Header("FMOD Event References")]
    [SerializeField] private EventReference doorOpenEvent;
    [SerializeField] private EventReference doorCloseEvent;
    [SerializeField] private EventReference doorNotOpeningEvent;

    private EventInstance currentInstance;

    private void StopCurrentSound()
    {
        if (currentInstance.isValid())
        {
            currentInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE); // 👈 Fully qualified
            currentInstance.release();
        }
    }

    private void PlaySound(EventReference soundEvent)
    {
        StopCurrentSound();
        currentInstance = RuntimeManager.CreateInstance(soundEvent);
        RuntimeManager.AttachInstanceToGameObject(currentInstance, gameObject); // ✅ Modern syntax
        currentInstance.start();
    }

    public void PlayDoorOpenSound()
    {

            PlaySound(doorOpenEvent);
        
        
    }

    public void PlayDoorCloseSound()
    {
        PlaySound(doorCloseEvent);
    }

    public void PlayDoorNotOpeningSound()
    {
        PlaySound(doorNotOpeningEvent);
    }
}
