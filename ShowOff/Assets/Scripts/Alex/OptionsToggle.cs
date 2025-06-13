using UnityEngine;

public class OptionsToggle : MonoBehaviour
{
    public GameObject mainMenuUI;  // Assign your main menu UI GameObject
    public GameObject optionsUI;   // Assign your options menu UI GameObject

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            bool isOptionsActive = optionsUI.activeSelf;

            if (isOptionsActive)
            {
                // Close options, open main menu
                optionsUI.SetActive(false);
                mainMenuUI.SetActive(true);
            }
            else
            {
                // Open options, close main menu
                optionsUI.SetActive(true);
                mainMenuUI.SetActive(false);
            }
        }
    }
}
