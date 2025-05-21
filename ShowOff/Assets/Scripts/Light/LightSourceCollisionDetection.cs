using System;
using UnityEngine;

public class LightSourceCollisionDetection : MonoBehaviour
{
    public static event Action<Light> OnLightDisabled;

    private Light lightComponent;

    void Start()
    {
        lightComponent = GetComponent<Light>();
    }

    void OnDisable()
    {
        // Fire event to unregister from the Player light collection
        OnLightDisabled?.Invoke(lightComponent);
    }
}