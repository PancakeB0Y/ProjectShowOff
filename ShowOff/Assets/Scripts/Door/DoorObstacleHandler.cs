using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.AI;

public class DoorObstacleHandler : MonoBehaviour
{
    [SerializeField]
    DoorController doorController;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<StatueController>(out StatueController statueController)
            && !doorController.isDoorOpen)
        {
            doorController.Interact();
        }
    }
}
