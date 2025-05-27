using UnityEngine;

public class StatueAudioController : MonoBehaviour
{
    private AudioSource audioSource;
    private float initialDistanceToTarget;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetupPlayerFollowAudio(float initialDistanceToTarget)
    {
        this.initialDistanceToTarget = initialDistanceToTarget;
        
        audioSource.Play();
    }

    public void AdjustAudioSourceVolume(float agentRemainingDistance)
    {
        // If correct remaining distance is not yet calculated
        if (agentRemainingDistance == 0f
            || agentRemainingDistance > initialDistanceToTarget)
            return;

        Debug.Log($"Remaining: {agentRemainingDistance}; Initial: {initialDistanceToTarget}");
        audioSource.volume = 1f - (Mathf.Clamp(agentRemainingDistance / initialDistanceToTarget, 0f, 1f));
    }

    public void StopPlaying()
    {
        audioSource.Stop();
        audioSource.volume = 0f;
    }
}
