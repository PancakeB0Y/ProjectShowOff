using UnityEngine;

public class OptionsCloseButton : MonoBehaviour
{
    public GameObject mainMenuUI;  // Assign your main menu UI GameObject
    public GameObject optionsUI;   // Assign your options menu UI GameObject

    // Call this method from the Options button OnClick()
    public void CloseOptions()
    {
        optionsUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}
