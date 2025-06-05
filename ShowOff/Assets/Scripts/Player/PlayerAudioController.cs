using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class PlayerAudioController : MonoBehaviour
{
    [Header("FMOD Event References")]
    [SerializeField] private EventReference playerWalkEvent;

    private EventInstance currentInstance;

    private void StopCurrentSound()
    {
        if (currentInstance.isValid())
        {
            currentInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            currentInstance.release();
        }
    }

    public void PlayWalkSound()
    {
        StopCurrentSound();
        currentInstance = RuntimeManager.CreateInstance(playerWalkEvent);
        RuntimeManager.AttachInstanceToGameObject(currentInstance, gameObject);
        
        currentInstance.start();
    }
}
