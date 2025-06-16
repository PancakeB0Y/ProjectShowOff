using System.Collections;
using UnityEngine;

public class LightSourceController : MonoBehaviour
{
    public bool IsLightOn = false;

    [SerializeField]
    private float turnLightOffSeconds = 2f;

    private GameObject lightSource;

    void Start()
    {
        lightSource = GetComponentInChildren<Light>(true).gameObject;
    }

    public void TurnLightOn(bool playSound = true)
    {
        lightSource.SetActive(true);

        StartCoroutine(TurnLanternOffCoroutine());
        IsLightOn = true;

        if (playSound)
        {
            SoundManager.instance.PlayCandleOnSound(gameObject);
        }
    }

    //Turn light off after X seconds - ONLY for testing
    IEnumerator TurnLanternOffCoroutine()
    {
        yield return new WaitForSeconds(turnLightOffSeconds);

        TurnLightOff();
    }

    public void TurnLightOff()
    {
        lightSource.SetActive(false);

        IsLightOn = false;
    }
}
