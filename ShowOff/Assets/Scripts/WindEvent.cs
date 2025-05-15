using UnityEngine;

public class WindEvent : MonoBehaviour
{
    SphereCollider windCollider;
    DebugSphere debugSphere;

    [Header("Debugging")]
    public bool debugRadius = true;

    [Header("Properties")]
    public bool destroyAfterTrigger = false;

    void Start()
    {
        windCollider = GetComponent<SphereCollider>();
        debugSphere = GetComponentInChildren<DebugSphere>();
    }

    private void Update()
    {
        if (!debugRadius)
        {
            return;
        }

        if (windCollider == null)
        {
            windCollider = GetComponent<SphereCollider>();
        }

        if (debugSphere == null) {
            debugSphere = GetComponentInChildren<DebugSphere>();
        }

        //Show collision radius at runtime
        debugSphere.gameObject.SetActive(debugRadius);

        float sphereRadius = windCollider.radius * 2;
        Vector3 sphereScale = new Vector3(sphereRadius, sphereRadius, sphereRadius);
        debugSphere.transform.localScale = sphereScale;
    }

    private void OnDrawGizmos()
    {
        if (!debugRadius)
        {
            return;
        }

        if (windCollider == null)
        {
            windCollider = GetComponent<SphereCollider>();
        }

        // Set the color with custom alpha.
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f); // Red with custom alpha

        // Draw the sphere.
        Gizmos.DrawSphere(transform.position, windCollider.radius);
    }

    public float GetColliderRadius()
    {
        if (windCollider == null)
        {
            windCollider = GetComponent<SphereCollider>();
        }

        return windCollider.radius;
    }

    public void UpdateColliderRadius(float radius)
    {
        if (windCollider == null)
        {
            windCollider = GetComponent<SphereCollider>();
        }

        windCollider.radius = radius;
    }

    public void triggerWind()
    {
        if (destroyAfterTrigger)
        {
            Destroy(gameObject);
        }
    }
}
