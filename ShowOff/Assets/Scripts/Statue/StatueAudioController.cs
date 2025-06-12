using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class StatueAudioController : MonoBehaviour
{
    [SerializeField] private EventReference statueFollowEvent;
    [SerializeField] private float maxAudioDistance = 15f; // Distance at which the parameter starts at 0
    [SerializeField] private float minAudioDistance = 2f;  // Distance at which the parameter reaches 1


    private EventInstance statueAudioInstance;
    private float initialDistanceToTarget;
    private bool isAudioPlaying = false;

    void Awake()
    {
        CreateAudioInstance();
    }

    private void CreateAudioInstance()
    {
        statueAudioInstance = RuntimeManager.CreateInstance(statueFollowEvent);
        RuntimeManager.AttachInstanceToGameObject(statueAudioInstance, gameObject);
        Debug.Log("FMOD audio instance created and attached.");
    }

    public void SetupPlayerFollowAudio(float initialDistanceToTarget)
    {
        Debug.Log($"SetupPlayerFollowAudio called. isAudioPlaying={isAudioPlaying}");
        this.initialDistanceToTarget = initialDistanceToTarget;

        if (!isAudioPlaying)
        {
            statueAudioInstance.start();
            isAudioPlaying = true;
            Debug.Log("FMOD audio started.");
        }
        else
        {
            Debug.LogWarning("Attempted to start audio, but it was already playing.");
        }
    }



    public void AdjustAudioDistanceParameter(Vector3 statuePosition, Vector3 playerPosition)
    {
        if (!isAudioPlaying) return;

        float distance = Vector3.Distance(statuePosition, playerPosition);

        // Normalize distance between maxAudioDistance and minAudioDistance
        float t = Mathf.InverseLerp(maxAudioDistance, minAudioDistance, distance);
        t = Mathf.Clamp01(t); // Ensures the value is between 0 and 1

        statueAudioInstance.setParameterByName("Distance", t);

        Debug.Log($"FMOD parameter 'Distance' set to {t} (Distance: {distance})");
    }



    public void StopPlaying()
    {
        Debug.Log($"StopPlaying called. isAudioPlaying={isAudioPlaying}");
        if (isAudioPlaying)
        {
            Debug.Log("Stopping and releasing FMOD audio.");
            statueAudioInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            statueAudioInstance.release();
            isAudioPlaying = false;

            // Create a new audio instance so we can start again later
            CreateAudioInstance();
        }
        else
        {
            Debug.LogWarning("Tried to stop audio but no audio was playing.");
        }
    }
}
