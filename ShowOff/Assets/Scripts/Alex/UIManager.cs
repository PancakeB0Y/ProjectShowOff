using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject pauseMenu;

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

        if(pauseMenu == null)
        {
            pauseMenu = GameObject.FindWithTag(pauseMenuTag);
        }
        
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }

    public bool IsPauseMenuOpen()
    {
        if(pauseMenu != null)
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
        }

        if (mainMenu != null) 
        {
            mainMenu.SetActive(false);
        }
    }

    public void ClosePauseMenu()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }

        if (mainMenu != null)
        {
            mainMenu.SetActive(true);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level test");
    }
    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
