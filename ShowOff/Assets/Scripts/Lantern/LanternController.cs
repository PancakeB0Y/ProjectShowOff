using System.Collections;
using UnityEngine;

public class LanternController : MonoBehaviour
{
    LightSourceController lightSourceController;

    [Header("Properties")]
    [SerializeField] float lightTime = 0.5f;

    public System.Action onTriggerWind;

    [SerializeField] bool useMatchsticks = false; //if false the lantern can be lit without using matchsticks

    void Start()
    {
        lightSourceController = GetComponentInChildren<LightSourceController>();
    }

    //Called when player presses button
    public void LightLantern()
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

            if (useMatchsticks) {
                //Check if the player has a matchstick
                //if (InventoryManager.instance.UseMatchstick())
                //{
                TurnLanternOn();
                //}
            }
            else
            {
                TurnLanternOn();
            }
        }
    }

    public bool CanLightLantern()
    {
        if (lightSourceController != null)
        {
            if (lightSourceController.IsLightOn)
            {
                return false;
            }

            return true;
        }

        return false;
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
                SoundManager.instance.PlayWindSound(gameObject);
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
        lightSourceController.IsLightOn = true;

        yield return new WaitForSeconds(lightTime);

        if (lightSourceController != null)
        {
            //Turn lantern on
            lightSourceController.TurnLightOn(false);
        }   
    }
}
