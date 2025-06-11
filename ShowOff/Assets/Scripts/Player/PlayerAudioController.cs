using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class PlayerAudioController : MonoBehaviour
{
    [Header("FMOD Event References")]
    [SerializeField] private EventReference playerWalkEvent;

    private EventInstance currentInstance;

    // Offset to place the footstep sound below the player
    [SerializeField] private Vector3 footstepOffset = new Vector3(0f, -1f, 0f);

    public void PlayWalkSound()
    {
        // Check if there is an existing event instance
        if (!currentInstance.isValid())
        {
            // Create new event instance
            currentInstance = RuntimeManager.CreateInstance(playerWalkEvent);

            // Attach the instance to the GameObject (new method)
            RuntimeManager.AttachInstanceToGameObject(currentInstance, gameObject);

            // Set the initial position (below player)
            currentInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position + footstepOffset));

            currentInstance.start();
            return;
        }

        // Check if the previous sound has ended
        currentInstance.getPlaybackState(out PLAYBACK_STATE playbackState);

        if (playbackState == PLAYBACK_STATE.STOPPED)
        {
            // Update position before replaying
            currentInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position + footstepOffset));
            currentInstance.start();
        }
    }
}
