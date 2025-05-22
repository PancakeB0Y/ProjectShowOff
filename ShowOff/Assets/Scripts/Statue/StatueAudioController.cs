using UnityEngine;

public class StatueAudioController : MonoBehaviour
{
    private AudioSource audioSource;
    private float initialDistanceToTarget;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
    }

    public void SetupPlayerFollowAudio(float initialDistanceToTarget)
    {
        this.initialDistanceToTarget = initialDistanceToTarget;
        
        audioSource.Play();
    }

    public void AdjustAudioSourceVolume(float agentRemainingDistance)
    {
        Debug.Log($"Remaining: {agentRemainingDistance}; Initial: {initialDistanceToTarget}");
        audioSource.volume = 1f - (Mathf.Clamp(agentRemainingDistance / initialDistanceToTarget, 0f, 1f));
    }

    public void StopPlaying()
    {
        audioSource.Stop();
    }
}
