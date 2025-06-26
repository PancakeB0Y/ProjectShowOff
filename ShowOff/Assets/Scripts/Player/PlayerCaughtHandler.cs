using System.Collections;
using FMODUnity;
using UnityEditor;
using UnityEngine;
using FMOD.Studio; 

public class PlayerCaughtHandler : MonoBehaviour
{
    [SerializeField] float waitSecondsBeforeDeath = 1.0f;

    PlayerController player;
    PlayerCameraController playerCameraController;
    Rigidbody rb;

    [SerializeField]
    Transform playerCamera;
    [SerializeField]
    LightSourceController lightSourceController;

    [SerializeField]
    EventReference deathSfx; 
    void Start()
    {   
        player = GetComponent<PlayerController>();
        playerCameraController = GetComponent<PlayerCameraController>();
        rb = GetComponent<Rigidbody>();
    }

    public void Die(Transform statue)
    {
        Debug.Log("In");

        RuntimeManager.PlayOneShot(deathSfx, transform.position);

        StatueAudioController statueAudio = statue.GetComponent<StatueAudioController>();
        if (statueAudio != null)
        {
            statueAudio.StopPlaying(); 
        }

        statue.TryGetComponent<StatueFaceSwap>(out StatueFaceSwap statueFaceSwap);
        if (statueFaceSwap != null) {
            statueFaceSwap.SetFace(3);
        }

        player.enabled = false;
        playerCameraController.enabled = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;

        transform.LookAt(statue);
        playerCamera.LookAt(statue.position + Vector3.up * 2);

        lightSourceController.TurnLightOn(false);

        StartCoroutine(WaitBeforeDeath());
    }

    IEnumerator WaitBeforeDeath()
    {
        yield return new WaitForSeconds(waitSecondsBeforeDeath);

        if (UIManager.Instance != null) {
            UIManager.Instance.OpenDeathMenu();
        }

#if UNITY_EDITOR
        //EditorApplication.isPlaying = false;
#endif
    }
}
