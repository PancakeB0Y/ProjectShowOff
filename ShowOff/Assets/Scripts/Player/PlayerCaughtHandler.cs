using System.Collections;
using UnityEditor;
using UnityEngine;

public class PlayerCaughtHandler : MonoBehaviour
{
    [SerializeField] float waitSecondsBeforeDeath = 1.0f;

    PlayerController player;
    PlayerCameraController playerCameraController;

    [SerializeField]
    Transform playerCamera;
    [SerializeField]
    LightSourceController lightSourceController;

    void Start()
    {   
        player = GetComponent<PlayerController>();
        playerCameraController = GetComponent<PlayerCameraController>();
    }

    public void Die(Transform statue)
    {
        Debug.Log("In");

        player.enabled = false;
        playerCameraController.enabled = false;

        transform.LookAt(statue);
        playerCamera.LookAt(statue.position + Vector3.up);

        lightSourceController.TurnLightOn(false);

        StartCoroutine(WaitBeforeDeath());
    }

    IEnumerator WaitBeforeDeath()
    {
        yield return new WaitForSeconds(waitSecondsBeforeDeath);

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
