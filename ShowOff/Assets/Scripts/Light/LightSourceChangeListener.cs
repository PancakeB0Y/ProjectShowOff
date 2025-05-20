using UnityEngine;

[ExecuteAlways]
public class LightSourceChangeListener : MonoBehaviour
{
    public Light targetLight;

    [HideInInspector] public float lastIntensity;
    [HideInInspector] public float lastRange;

    [SerializeField]
    private float minVisibleIntensity = 0.05f;
    private SphereCollider collisionTrigger;

    void Start()
    {
        targetLight = GetComponent<Light>();
        collisionTrigger = GetComponent<SphereCollider>();
    }

    public void OnLightValuesChanged()
    {
        if (collisionTrigger == null) {
            collisionTrigger = GetComponent<SphereCollider>();
        }

        if (collisionTrigger != null) {
            //Debug.Log($"[LightChangeListener] Light changed: Intensity = {targetLight.intensity}, Range = {targetLight.range}");

            float effectiveDistance = Mathf.Sqrt(targetLight.intensity / minVisibleIntensity);
            effectiveDistance = Mathf.Min(effectiveDistance, targetLight.range);

            collisionTrigger.radius = effectiveDistance;
        }
    }
}
