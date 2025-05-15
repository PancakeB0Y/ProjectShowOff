using UnityEngine;

public class LightSource : MonoBehaviour
{
    Light pointLight;
    SphereCollider lightCollider;

    [HideInInspector] public bool isLightOn = true;

    void Start()
    {
        pointLight = GetComponent<Light>();
        lightCollider = GetComponent<SphereCollider>();

        if(isLightOn)
        {
            TurnLightOn();
        }
    }

    public float GetColliderRadius()
    {
        if (lightCollider == null)
        {
            lightCollider = GetComponent<SphereCollider>();
        }

        return lightCollider.radius;
    }

    public void UpdateColliderRadius(float radius)
    {
        if (lightCollider == null)
        {
            lightCollider = GetComponent<SphereCollider>();
        }

        lightCollider.radius = radius;
        UpdateLightRadius(radius);
    }

    private void UpdateLightRadius(float radius)
    {
        if(pointLight == null)
        {
            pointLight = GetComponent<Light>();
        }

        pointLight.range = radius;
        pointLight.intensity = radius;
    }

    public void TurnLightOn()
    {
        if(pointLight == null)
        {
            pointLight = GetComponent<Light>();
        }

        pointLight.enabled = true;
        isLightOn = true;
    }

    public void TurnLightOff()
    {
        if (pointLight == null)
        {
            pointLight = GetComponent<Light>();
        }

        pointLight.enabled = false;
        isLightOn = false;
    }
}
