using System.Collections;
using UnityEditor;
using UnityEngine;

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

    void Start()
    {   
        player = GetComponent<PlayerController>();
        playerCameraController = GetComponent<PlayerCameraController>();
        rb = GetComponent<Rigidbody>();
    }

    public void Die(Transform statue)
    {
        Debug.Log("In");

        statue.TryGetComponent<StatueFaceSwap>(out StatueFaceSwap statueFaceSwap);
        if (statueFaceSwap != null) {
            statueFaceSwap.SetFace(3);
        }

        player.enabled = false;
        playerCameraController.enabled = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;

        transform.LookAt(statue);
        playerCamera.LookAt(statue.position + Vector3.up);

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
