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
            if (lightSourceController == null)
            {
                lightSourceController = GetComponentInChildren<LightSourceController>();
            }

            if (lightSourceController != null)
            {
                if (lightSourceController.IsLightOn)
                {
                    return;
                }

                TurnLanternOn();

                if (SoundManager.instance != null)
                {
                    SoundManager.instance.PlayLightMatchSound();
                }
            }
        }
    }

    //Called when player collides with wind
    public void HandleWindCollision()
    {
        if (lightSourceController == null)
        {
            lightSourceController = GetComponentInChildren<LightSourceController>();
        }

        //Check if the lantern is turned on
        if (lightSourceController.IsLightOn)
        {
            //Turn lantern off
            TurnLanternOff();

            //Inform wind sphere object
            onTriggerWind?.Invoke();

            if (SoundManager.instance != null) {
                SoundManager.instance.PlayWindSound();
            }
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
