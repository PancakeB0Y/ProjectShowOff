using System.Collections;
using UnityEngine;

public class LanternController : MonoBehaviour
{
    LightSourceController lightSourceController;

    [Header("Properties")]
    [SerializeField] float lightTime = 0.5f;

    public System.Action onTriggerWind;

    void Start()
    {
        lightSourceController = GetComponentInChildren<LightSourceController>();
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
        
        if (lightSourceController == null)
        {
            lightSourceController = GetComponentInChildren<LightSourceController>();
        }

        //Check if the lantern is turned on
        if (lightSourceController.IsLightOn) {
            //Turn lantern off
            TurnLanternOff();

            //Inform wind object
            onTriggerWind?.Invoke();
        }
    }

    void TurnLanternOn()
    {
        if (lightSourceController == null)
        {
            lightSourceController = GetComponentInChildren<LightSourceController>();
        }

        if (!lightSourceController.IsLightOn)
        {
            StartCoroutine(TurnLanternOnCoroutine());
        }
    }

    void TurnLanternOff()
    {
        if (lightSourceController != null)
        {
            lightSourceController.TurnLightOff();
        }
    }

    //Light the lantern after X seconds
    IEnumerator TurnLanternOnCoroutine()
    {
        yield return new WaitForSeconds(lightTime);

        if (lightSourceController != null)
        {
            //Turn lantern on
            lightSourceController.TurnLightOn();
        }   
    }
}
