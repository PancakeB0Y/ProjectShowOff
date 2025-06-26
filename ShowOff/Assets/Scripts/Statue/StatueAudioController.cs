using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class StatueAudioController : MonoBehaviour
{
    [Header("Statue Audio Settings")]
    [SerializeField] private EventReference statueFollowEvent;
    [SerializeField] private float maxAudioDistance = 15f;
    [SerializeField] private float minAudioDistance = 2f;
    [SerializeField] private LayerMask occlusionLayers;

    [Header("Violin Audio Settings")]
    [SerializeField] private EventReference violinIntenseEvent;
    [SerializeField] private EventReference violinCalmEvent;
    [SerializeField] private float violinMaxAudioDistance = 20f;
    [SerializeField] private float violinMinAudioDistance = 3f;

    private EventInstance statueAudioInstance;
    private EventInstance violinIntenseInstance;
    private EventInstance violinCalmInstance;

    private bool isAudioPlaying = false;
    private bool isViolinPlaying = false;

    [HideInInspector] public Transform playerTransform;

    void Awake()
    {
        CreateAudioInstances();
    }

    void Update()
    {
        if (isAudioPlaying && playerTransform != null)
        {
            HandleStatueAudio();
            HandleViolinAudio();
        }
    }

    private void HandleStatueAudio()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        float t = Mathf.InverseLerp(maxAudioDistance, minAudioDistance, distance);
        t = Mathf.Clamp01(t);

        Vector3 direction = playerTransform.position - transform.position;
        bool occluded = Physics.Raycast(transform.position, direction.normalized, out RaycastHit hit, distance, occlusionLayers);

        float occlusionParam = occluded ? 0.2f : 1.0f;

        statueAudioInstance.setParameterByName("Distance", t);
        statueAudioInstance.setParameterByName("Occlusion", occlusionParam);

        Debug.Log($"Statue FMOD 'Distance' param: {t}, Occlusion: {occlusionParam}, Distance: {distance}, Occluded: {occluded}");
    }

    private void HandleViolinAudio()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        float t = Mathf.InverseLerp(violinMaxAudioDistance, violinMinAudioDistance, distance);
        t = Mathf.Clamp01(t);

        // Invert the t value
        float invertedT = 1f - t;

        // Send the inverted parameter to both events
        violinIntenseInstance.setParameterByName("Distance New", invertedT);
        violinCalmInstance.setParameterByName("Distance New", invertedT);

        Debug.Log($"Violin Distance New Param (Inverted): {invertedT}, Distance: {distance}");
    }

    private void CreateAudioInstances()
    {
        statueAudioInstance = RuntimeManager.CreateInstance(statueFollowEvent);
        RuntimeManager.AttachInstanceToGameObject(statueAudioInstance, gameObject);

        violinIntenseInstance = RuntimeManager.CreateInstance(violinIntenseEvent);
        RuntimeManager.AttachInstanceToGameObject(violinIntenseInstance, gameObject);

        violinCalmInstance = RuntimeManager.CreateInstance(violinCalmEvent);
        RuntimeManager.AttachInstanceToGameObject(violinCalmInstance, gameObject);

        Debug.Log("FMOD audio instances created and attached.");
    }

    public void SetupPlayerFollowAudio()
    {
        if (!isAudioPlaying)
        {
            statueAudioInstance.start();
            isAudioPlaying = true;
            Debug.Log("FMOD statue audio started.");
        }
        else
        {
            Debug.LogWarning("Statue audio already playing.");
        }

        if (!isViolinPlaying)
        {
            violinIntenseInstance.start();
            violinCalmInstance.start();
            isViolinPlaying = true;
            Debug.Log("FMOD violin audio started.");
        }
        else
        {
            Debug.LogWarning("Violin audio already playing.");
        }
    }

    public void StopPlaying()
    {
        if (isAudioPlaying)
        {
            statueAudioInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); 
            statueAudioInstance.release();
            isAudioPlaying = false;
            Debug.Log("FMOD statue audio stopped and released.");
        }

        if (isViolinPlaying)
        {
            violinIntenseInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); 
            violinIntenseInstance.release();

            violinCalmInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            violinCalmInstance.release();

            isViolinPlaying = false;
            Debug.Log("FMOD violin audio stopped and released.");
        }

        CreateAudioInstances(); 
    }

}
