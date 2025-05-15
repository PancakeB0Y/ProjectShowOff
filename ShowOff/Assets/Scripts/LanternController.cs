using System.Collections;
using UnityEngine;

public class LanternController : MonoBehaviour
{
    LightSource lightSource;

    readonly string windTag = "Wind";

    [Header("Properties")]
    [SerializeField] float lightTime = 0.5f;

    void Start()
    {
        lightSource = GetComponentInChildren<LightSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            LightLantern();
        }
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

    void LightLantern()
    {
        if (lightSource == null)
        {
            lightSource = GetComponentInChildren<LightSource>();
        }

        if (!lightSource.isLightOn)
        {
            StartCoroutine(LightLanternCoroutine());
        }
    }

    IEnumerator LightLanternCoroutine()
    {
        yield return new WaitForSeconds(lightTime);

        if (lightSource == null)
        {
            lightSource = GetComponentInChildren<LightSource>();
        }

        //Turn lantern on
        lightSource.TurnLightOn();
    }
}
