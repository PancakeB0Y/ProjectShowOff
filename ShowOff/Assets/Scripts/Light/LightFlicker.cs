using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour
{
    public float baseIntensity = 1.5f;
    public float flickerAmount = 0.5f;
    public float flickerSpeed = 2.0f;

    private Light flickeringLight;
    private float seed;

    void Start()
    {
        flickeringLight = GetComponent<Light>();
        seed = Random.Range(0f, 100f);
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(seed, Time.time * flickerSpeed);
        flickeringLight.intensity = baseIntensity + (noise - 0.5f) * flickerAmount;
    }
}