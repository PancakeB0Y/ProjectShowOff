using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections;

public class AmbientZoneTrigger : MonoBehaviour
{
    [Header("FMOD Event Reference")]
    [SerializeField] private EventReference ambientLayerEvent;

    private EventInstance ambientInstance;
    private Coroutine fadeCoroutine;
    private int playerInsideCount = 0; 

    private void Start()
    {
        if (!ambientLayerEvent.IsNull)
        {
            ambientInstance = RuntimeManager.CreateInstance(ambientLayerEvent);
            ambientInstance.setParameterByName("ZoneIntensity", 0f);
            ambientInstance.start();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideCount++;

            if (playerInsideCount == 1 && ambientInstance.isValid()) 
            {
                if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
                fadeCoroutine = StartCoroutine(FadeParameter("ZoneIntensity", 1f, 2f)); 
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideCount = Mathf.Max(0, playerInsideCount - 1); 

            if (playerInsideCount == 0 && ambientInstance.isValid()) 
            {
                if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
                fadeCoroutine = StartCoroutine(FadeParameter("ZoneIntensity", 0f, 4f)); 
            }
        }
    }

    private IEnumerator FadeParameter(string paramName, float targetValue, float duration)
    {
        float currentValue;
        ambientInstance.getParameterByName(paramName, out currentValue);
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float newValue = Mathf.Lerp(currentValue, targetValue, timer / duration);
            ambientInstance.setParameterByName(paramName, newValue);
            yield return null;
        }

        ambientInstance.setParameterByName(paramName, targetValue);
    }

    private void OnDestroy()
    {
        if (ambientInstance.isValid())
        {
            ambientInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            ambientInstance.release();
        }
    }

    public void NotifyPlayerEntered()
    {
        playerInsideCount++;

        if (playerInsideCount == 1 && ambientInstance.isValid())
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeParameter("ZoneIntensity", 1f, 2f));
        }
    }

    public void NotifyPlayerExited()
    {
        playerInsideCount = Mathf.Max(0, playerInsideCount - 1);

        if (playerInsideCount == 0 && ambientInstance.isValid())
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeParameter("ZoneIntensity", 0f, 4f));
        }
    }
}
