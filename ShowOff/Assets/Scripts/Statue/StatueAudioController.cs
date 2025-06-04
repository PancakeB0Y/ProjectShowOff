using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class StatueAudioController : MonoBehaviour
{
    [SerializeField] private EventReference statueFollowEvent;

    private EventInstance statueAudioInstance;
    private float initialDistanceToTarget;

    void Awake()
    {
        // Create FMOD instance and attach to the GameObject
        statueAudioInstance = RuntimeManager.CreateInstance(statueFollowEvent);
        RuntimeManager.AttachInstanceToGameObject(statueAudioInstance, gameObject);
    }

    public void SetupPlayerFollowAudio(float initialDistanceToTarget)
    {
        this.initialDistanceToTarget = initialDistanceToTarget;
        statueAudioInstance.start();
    }

    public void AdjustAudioSourceVolume(float agentRemainingDistance)
    {
        if (agentRemainingDistance == 0f || agentRemainingDistance > initialDistanceToTarget)
            return;

        float normalizedVolume = 1f - Mathf.Clamp(agentRemainingDistance / initialDistanceToTarget, 0f, 1f);
        statueAudioInstance.setVolume(normalizedVolume);

        Debug.Log($"Remaining: {agentRemainingDistance}; Initial: {initialDistanceToTarget}; Volume: {normalizedVolume}");
    }

    public void StopPlaying()
    {
        statueAudioInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        statueAudioInstance.release();
    }
}
