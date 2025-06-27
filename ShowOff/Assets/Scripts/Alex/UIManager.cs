using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Main Menu Scene")]
    [SerializeField] GameObject mainMenuCanvas;

    [Header("Game Scene")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject optionsPanel;
    [SerializeField] GameObject deathMenu;
    [SerializeField] GameObject winMenu;

    [Header("Help Panels")]
    [SerializeField] GameObject helpPanelMainMenu;
    [SerializeField] GameObject helpPanelGame;

    string pauseMenuTag = "PauseMenu";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
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

        if (helpPanelMainMenu != null)
        {
            helpPanelMainMenu.SetActive(false);
        }

        if (helpPanelGame != null)
        {
            helpPanelGame.SetActive(false);
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

        // disable cursor
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("FinalScene");
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

    // MAIN MENU - HELP METHODS

    public void OpenHelpFromMainMenu()
    {
        if (helpPanelMainMenu != null)
            helpPanelMainMenu.SetActive(true);

        if (mainMenuCanvas != null)
            mainMenuCanvas.SetActive(false);
    }

    public void CloseHelpToMainMenu()
    {
        if (helpPanelMainMenu != null)
            helpPanelMainMenu.SetActive(false);

        if (mainMenuCanvas != null)
            mainMenuCanvas.SetActive(true);
    }

    // GAME SCENE - HELP METHODS

    public void OpenHelpFromPauseMenu()
    {
        if (helpPanelGame != null)
            helpPanelGame.SetActive(true);

        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (optionsPanel != null)
            optionsPanel.SetActive(false);
    }

    public void CloseHelpToPauseMenu()
    {
        if (helpPanelGame != null)
            helpPanelGame.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(true);
    }


    public void OpenDeathMenu()
    {
        if(deathMenu != null)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
            deathMenu.SetActive(true);

            if(deathMenu.TryGetComponent<DeathMenu>(out DeathMenu controller))
            {
                controller.FadeBackground();
            }
        }
    }

    public void CloseDeathMenu()
    {
        if (deathMenu != null)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
            deathMenu.SetActive(false);
        }
    }

    public void OpenWinMenu()
    {
        if (winMenu != null)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
            winMenu.SetActive(true);
        }
    }

    public void CloseWinMenu()
    {
        if (winMenu != null)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
            winMenu.SetActive(false);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
