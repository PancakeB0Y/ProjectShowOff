using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class StatueAudioController : MonoBehaviour
{
    [SerializeField] private EventReference statueFollowEvent;
    [SerializeField] private float maxAudioDistance = 15f;
    [SerializeField] private float minAudioDistance = 2f;
    [SerializeField] private LayerMask occlusionLayers;

    private EventInstance statueAudioInstance;
    private bool isAudioPlaying = false;

    [HideInInspector] public Transform playerTransform;

    void Awake()
    {
        CreateAudioInstance();
    }

    void Update()
    {
        if (!isAudioPlaying || playerTransform == null)
            return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);
        float t = Mathf.InverseLerp(maxAudioDistance, minAudioDistance, distance);
        t = Mathf.Clamp01(t);

        Vector3 direction = playerTransform.position - transform.position;
        bool occluded = Physics.Raycast(transform.position, direction.normalized, out RaycastHit hit, distance, occlusionLayers);

        float occlusionParam = occluded ? 0.2f : 1.0f;

        statueAudioInstance.setParameterByName("Distance", t);
        statueAudioInstance.setParameterByName("Occlusion", occlusionParam);

        Debug.Log($"FMOD 'Distance' param: {t}, Occlusion: {occlusionParam}, Distance: {distance}, Occluded: {occluded}");
    }

    private void CreateAudioInstance()
    {
        statueAudioInstance = RuntimeManager.CreateInstance(statueFollowEvent);
        RuntimeManager.AttachInstanceToGameObject(statueAudioInstance, gameObject);  // <-- fixed here
        Debug.Log("FMOD audio instance created and attached.");
    }

    public void SetupPlayerFollowAudio()
    {
        if (!isAudioPlaying)
        {
            statueAudioInstance.start();
            isAudioPlaying = true;
            Debug.Log("FMOD audio started.");
        }
        else
        {
            Debug.LogWarning("Audio already playing.");
        }
    }

    public void StopPlaying()
    {
        if (isAudioPlaying)
        {
            statueAudioInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);  // <-- fully qualified STOP_MODE here
            statueAudioInstance.release();
            isAudioPlaying = false;
            CreateAudioInstance();
            Debug.Log("FMOD audio stopped and released.");
        }
        else
        {
            Debug.LogWarning("Audio was not playing.");
        }
    }
}
