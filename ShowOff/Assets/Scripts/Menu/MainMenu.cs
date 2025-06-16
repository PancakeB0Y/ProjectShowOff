using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;    // Assign the main menu canvas or panel here
    public GameObject optionsUI;     // Assign the options canvas or panel here

    public void StartGame()
    {
        SceneManager.LoadScene("Level test");
    }

    public void OpenOptions()
    {
        mainMenuUI.SetActive(false);
        optionsUI.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}