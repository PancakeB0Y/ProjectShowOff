using System.Collections;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image backgroundImage;
    [SerializeField] GameObject menuButtons;
    [SerializeField] float fadeTime = 0.01f;

    public void FadeBackground()
    {
        StartCoroutine(FadeBackgroundCoroutine());
    }

    IEnumerator FadeBackgroundCoroutine()
    {
        while (backgroundImage.color.a < 1)
        {
            Debug.Log(backgroundImage.color.a);
            Color backgroundColor = backgroundImage.color;
            backgroundColor.a += 0.005f;
            backgroundImage.color = backgroundColor;

            yield return new WaitForSeconds(fadeTime);
        }

        menuButtons.SetActive(true);
    }
}
