using UnityEngine;

public class LightUpTriggerDetection : MonoBehaviour
{
    private LightSourceController lightSourceParent;

    void Start()
    {
        lightSourceParent = transform.parent.GetComponent<LightSourceController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!lightSourceParent.IsLightOn
            && other.TryGetComponent<LightUpTriggerDetection>(out LightUpTriggerDetection lightUpTrigger)
            && lightUpTrigger.lightSourceParent.IsLightOn)
        {
            lightSourceParent.TurnLightOn();
        }
    }
}