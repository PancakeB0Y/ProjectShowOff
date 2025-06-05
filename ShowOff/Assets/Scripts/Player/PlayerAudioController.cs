using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using Mono.Cecil;
using EventReference = FMODUnity.EventReference;

public class PlayerAudioController : MonoBehaviour
{
    [Header("FMOD Event References")]
    [SerializeField] private EventReference playerWalkEvent;

    private EventInstance currentInstance;

    public void PlayWalkSound()
    {
        // check if there is an existing event instance
        if (!currentInstance.isValid())
        {
            // create new event instance
            currentInstance = RuntimeManager.CreateInstance(playerWalkEvent);
            RuntimeManager.AttachInstanceToGameObject(currentInstance, gameObject);

            currentInstance.start();

            return;
        }

        // check if the previous sound has ended
        PLAYBACK_STATE playbackState;
        currentInstance.getPlaybackState(out playbackState);

        if (playbackState == PLAYBACK_STATE.STOPPED)
        {
            currentInstance.start();
        }   
    }
}
