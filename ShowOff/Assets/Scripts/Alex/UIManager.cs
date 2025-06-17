using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    [Header("Main Menu Scene")]
    [SerializeField] GameObject mainMenuCanvas;

    [Header("Game Scene")]
    [SerializeField] GameObject pauseMenu;      
    [SerializeField] GameObject pausePanel;     
    [SerializeField] GameObject optionsPanel;   

    string pauseMenuTag = "PauseMenu";

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        if (pauseMenu == null)
        {
            pauseMenu = GameObject.FindWithTag(pauseMenuTag);
        }

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false);
        }
    }

    public bool IsPauseMenuOpen()
    {
        if (pauseMenu != null)
        {
            return pauseMenu.activeSelf;
        }

        return false;
    }

    public void OpenPauseMenu()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;

            if (pausePanel != null)
                pausePanel.SetActive(true);

            if (optionsPanel != null)
                optionsPanel.SetActive(false);
        }

        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(false);
        }
    }

    public void ClosePauseMenu()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }

        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(true);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level test");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Main menu");
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }


    // MAIN MENU - OPTIONS METHODS

    public void OpenOptionsFromMainMenu()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(true);
        }

        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(false);
        }
    }

    public void CloseOptionsToMainMenu()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false);
        }

        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(true);
        }
    }

    // GAME SCENE - OPTIONS METHODS

    public void OpenOptionsFromPauseMenu()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        if (optionsPanel != null)
        {
            optionsPanel.SetActive(true);
        }
    }

    public void CloseOptionsToPauseMenu()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }

        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false);
        }
    }
}
