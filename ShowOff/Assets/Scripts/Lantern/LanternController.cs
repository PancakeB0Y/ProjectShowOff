using System.Collections;
using UnityEngine;

public class LanternController : MonoBehaviour
{
    LightSource lightSource;

    [Header("Properties")]
    [SerializeField] float lightTime = 0.5f;

    public System.Action onTriggerWind;

    void Start()
    {
        lightSource = GetComponentInChildren<LightSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TurnLanternOn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<WindSphere>(out WindSphere wind))
        {
            return;
        }
        
        if (lightSource == null)
        {
            lightSource = GetComponentInChildren<LightSource>();
        }

        //Check if the lantern is turned on
        if (lightSource.isLightOn)
        {
            //Turn lantern off
            TurnLanternOff();

            //Inform wind object
            onTriggerWind?.Invoke();
        }
    }

    void TurnLanternOn()
    {
        if (lightSource == null)
        {
            lightSource = GetComponentInChildren<LightSource>();
        }

        if (!lightSource.isLightOn)
        {
            StartCoroutine(TurnLanternOnCoroutine());
        }
    }

    void TurnLanternOff()
    {
        if (lightSource != null)
        {
            lightSource.TurnLightOff();
        }
    }

    //Light the lantern after X seconds
    IEnumerator TurnLanternOnCoroutine()
    {
        yield return new WaitForSeconds(lightTime);

        if (lightSource != null)
        {
            //Turn lantern on
            lightSource.TurnLightOn();
        }   
    }
}
