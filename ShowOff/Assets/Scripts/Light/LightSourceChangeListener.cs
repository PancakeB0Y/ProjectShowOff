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
        EnsureSetup();
    }

    void OnEnable()
    {
        EnsureSetup();
    }

    void OnValidate()
    {
        EnsureSetup();
    }

    private void EnsureSetup()
    {
        // Don't override if manually set
        if (targetLight == null)
            targetLight = GetComponent<Light>();

        if (collisionTrigger == null)
            collisionTrigger = GetComponent<SphereCollider>();
    }

    public void OnLightValuesChanged()
    {
        if (collisionTrigger == null) {
            collisionTrigger = GetComponent<SphereCollider>();
        }

        if (collisionTrigger != null) {
            //Debug.Log($"[LightChangeListener] Light changed: Intensity = {targetLight.intensity}, Range = {targetLight.range}");

        collisionTrigger.radius = CalculateEffectiveVisibilityDistance(targetLight.intensity, minVisibleIntensity, targetLight.range);
    }

    /// <summary>
    /// Approximates URP-style attenuation falloff to find at what distance the intensity drops to 'threshold'.
    /// </summary>
    private float CalculateEffectiveVisibilityDistance(float intensity, float threshold, float range)
    {
        if (threshold >= intensity)
            return 0f;

        float normalized = Mathf.Sqrt(1.0f - Mathf.Clamp01(threshold / intensity));
        return normalized * range;
    }
}
