using UnityEngine;

public class LanternController : MonoBehaviour
{
    LightSource lightSource;

    string windTag = "Wind";
    
    void Start()
    {
        lightSource = GetComponentInChildren<LightSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(windTag))
        {
            return;
        }

        if (lightSource == null)
        {
            lightSource = GetComponentInChildren<LightSource>();
        }

        //Turn lantern off
        lightSource.TurnLightOff();

        //Destroy the wind object if needed
        WindEvent wind = other.GetComponent<WindEvent>();
        if (wind != null) {
            wind.triggerWind();
        }
    }
}
