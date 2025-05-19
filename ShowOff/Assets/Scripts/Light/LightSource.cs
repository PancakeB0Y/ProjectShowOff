using UnityEngine;

public class LightSource : MonoBehaviour
{
    [SerializeField] private float radius = 1f;

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
    private void OnEnable()
    {
        SyncComponents();
    }

    //Called on inspector changes
    private void OnValidate()
    {
        SyncComponents();
    }

    //Updates the collider and light based on radius 
    private void SyncComponents()
    {
        if (pointLight == null)
        {
            pointLight = GetComponent<Light>();
        }

        if (lightCollider == null) { 
            lightCollider = GetComponent<SphereCollider>();
        }

        if (pointLight != null)
        {
            pointLight.range = radius;
            pointLight.intensity = radius;
        }

        if (lightCollider != null)
        {
            lightCollider.radius = radius;
        }
    }

    public float GetRadius()
    {
        return radius;
    }

    public void UpdateRadius(float newRadius)
    {
        radius = newRadius;
        SyncComponents();
    }

    public void TurnLightOn()
    {
        if(pointLight == null)
        {
            pointLight = GetComponent<Light>();
        }

        if (pointLight != null)
        {
            pointLight.enabled = true;
            isLightOn = true;
        }
    }

    public void TurnLightOff()
    {
        if (pointLight == null)
        {
            pointLight = GetComponent<Light>();
        }

        if (pointLight != null)
        {
            pointLight.enabled = false;
            isLightOn = false;
        }
    }
}
