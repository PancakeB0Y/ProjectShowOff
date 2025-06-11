using System.Collections.Generic;
using UnityEngine;

public class LanternCollDetector : MonoBehaviour
{
    private readonly HashSet<Collider> activeWallContacts = new();

    public bool IsTouchingWall => activeWallContacts.Count > 0;
    [HideInInspector] public Vector3 pushVector;

    void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
            activeWallContacts.Add(other);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("LanternCollidable"))
        {
            Vector3 penetrationDirection;
            float penetrationDistance;

            // Calculate how deep the trigger is in the wall
            bool isOverlapping = Physics.ComputePenetration(
                GetComponent<Collider>(), transform.position, transform.rotation,
                other, other.transform.position, other.transform.rotation,
                out penetrationDirection, out penetrationDistance
            );

            if (isOverlapping)
            {
                // Move the lantern away from the wall proportionally to the penetration
                pushVector = penetrationDirection * penetrationDistance;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger)
            activeWallContacts.Remove(other);
    }
}