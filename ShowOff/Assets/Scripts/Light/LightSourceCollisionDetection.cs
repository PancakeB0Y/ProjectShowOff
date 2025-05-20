using UnityEngine;

public class LightSourceCollisionDetection : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<StatueController>(out StatueController statue))
        {
            statue.StopMovement();
        }
    }
}