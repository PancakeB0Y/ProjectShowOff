using UnityEngine;

public class PointLightScaler : MonoBehaviour
{
    Light pointLight;
    SphereCollider lightCollider;

    void Start()
    {
        pointLight = GetComponent<Light>();
        lightCollider = GetComponent<SphereCollider>();
    }

    public float GetLightRadius()
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
}
