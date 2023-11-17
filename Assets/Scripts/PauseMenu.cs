using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button pauseButton; 
    public Button resumeButton;
    public Button mainMenuButton;
    public Button quitButton;
    public KeyCode pauseKey = KeyCode.Escape;

    void Start()
    {
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(TogglePauseMenu);
        }

        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(ResumeGame);
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            TogglePauseMenu();
        }
    }

    void TogglePauseMenu()
    {
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);

        Time.timeScale = pauseMenuUI.activeSelf ? 0f : 1f;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);

        Time.timeScale = 1f;
    }

    void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");

        Time.timeScale = 1f;
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
