using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour
{
    public float baseIntensity = 1.5f;
    public float flickerAmount = 0.5f;
    public float flickerSpeed = 2.0f;

    public float intensityIncreaseAmount = 0.001f;

    private Light flickeringLight;
    private float seed;

    private LightMode lightMode = LightMode.Normal;
    private float currentIntensity = 5.0f;

    void Start()
    {
        flickeringLight = GetComponent<Light>();
        seed = Random.Range(0f, 100f);

        lightMode = LightMode.Brightening;
    }

    void Update()
    {
        if (lightMode == LightMode.Brightening)
        {
            //Debug.Log(currentIntensity);
            currentIntensity += intensityIncreaseAmount;
            if (currentIntensity > baseIntensity)
            {
                currentIntensity = baseIntensity;
                lightMode = LightMode.Normal;
            }
        }

        float noise = Mathf.PerlinNoise(seed, Time.time * flickerSpeed);
        flickeringLight.intensity = currentIntensity + (noise - 0.5f) * flickerAmount;
    }

    void OnEnable()
    {
        Debug.Log("Called");
        lightMode = LightMode.Brightening;
        currentIntensity = 0.0f;
    }
}

public enum LightMode
{
    Brightening,
    Normal
}