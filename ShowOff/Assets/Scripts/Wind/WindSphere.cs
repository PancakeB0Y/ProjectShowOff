using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

//Wind which extinguishes lantern
public class WindSphere : MonoBehaviour
{
    SphereCollider windCollider;
    DebugSphere debugSphere; //sphere to show radius

    [Header("Properties")]
    [SerializeField] private float radius = 1f;
    [SerializeField] bool destroyAfterTrigger = false;

    [Header("Debugging")]
    [SerializeField] public bool debugRadius = true;

    LanternController lantern;

    void Awake()
    {
        lantern = FindAnyObjectByType<LanternController>();
    }

    void Start()
    {
        windCollider = GetComponent<SphereCollider>();
        debugSphere = GetComponentInChildren<DebugSphere>();
    }

    //Called on inspector changes
    private void OnValidate()
    {
        SyncComponents();
    }

    private void OnEnable()
    {
        if (lantern != null) {
            lantern.onTriggerWind += triggerWind;
        }

        SyncComponents();
    }

    private void OnDisable()
    {
        if (lantern != null)
        {
            lantern.onTriggerWind -= triggerWind;
        }
    }

    //Updates the collider and debugSphere based on radius 
    private void SyncComponents()
    {
        //Sync collider
        if (windCollider == null)
        {
            windCollider = GetComponent<SphereCollider>();
        }

        if (windCollider != null)
        {
            windCollider.radius = radius;
        }

        //Sync debugSphere
        if (debugSphere == null)
        {
            debugSphere = GetComponentInChildren<DebugSphere>();
        }

        if (debugSphere != null)
        {
            //Show collision radius at runtime
            debugSphere.SetVisibility(debugRadius);

            //Scale sphere if visible
            if (debugRadius)
            {
                float sphereRadius = radius * 2;
                debugSphere.UpdateRadius(sphereRadius);
            }
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

    public void triggerWind()
    {
        if (destroyAfterTrigger)
        {
            Destroy(gameObject);
        }
    }
}
