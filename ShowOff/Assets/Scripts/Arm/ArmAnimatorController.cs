using UnityEngine;

public class ArmAnimatorController : MonoBehaviour
{
    Animator lightMatchAnimator;

    LanternController lanternController;
    LightSourceController lanternLightSourceController;

    [SerializeField] GameObject matchstick;
    [SerializeField] Light matchstickLight;

    void Start()
    {
        lightMatchAnimator = GetComponent<Animator>();

        lanternController = transform.parent.GetComponentInChildren<LanternController>();
        lanternLightSourceController = transform.parent.GetComponentInChildren<LightSourceController>();

        if (!matchstickLight)
            matchstickLight = GetComponentInChildren<Light>();
    }

    public void StartAnimation()
    {
        if (!lanternLightSourceController.IsLightOn)
            lightMatchAnimator.SetBool("lightmatch", true);
    }

    public void matchstickon()
    {
        matchstick.SetActive(true);
    }

    public void matchlighton()
    {
        matchstickLight.enabled = true;
        SoundManager.instance.PlayLightLanternSound(gameObject);
    }

    public void lanternon()
    {
        lanternController.LightLantern();
    }

    public void matchlightoff()
    {
        matchstickLight.enabled = false;
    }

    public void matchstickoff()
    {
        matchstick.SetActive(false);

        lightMatchAnimator.SetBool("lightmatch", false);
    }
}
