using UnityEngine;

public class ZoneTriggerPart : MonoBehaviour
{
    public AmbientZoneTrigger parentZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parentZone?.NotifyPlayerEntered();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parentZone?.NotifyPlayerExited();
        }
    }
}
