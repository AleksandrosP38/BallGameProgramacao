using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button pauseButton; // Reference to your UI button
    public Button resumeButton;
    public Button mainMenuButton;
    public Button quitButton;
    public KeyCode pauseKey = KeyCode.Escape;

    void Start()
    {
        // Add a listener to the pause button
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(TogglePauseMenu);
        }

        // Add listeners to the other buttons
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
        // Check if the player presses the pause button
        if (Input.GetKeyDown(pauseKey))
        {
            TogglePauseMenu();
        }
    }

    void TogglePauseMenu()
    {
        // Toggle the pause menu visibility
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);

        // Toggle the time scale between 0 and 1
        Time.timeScale = pauseMenuUI.activeSelf ? 0f : 1f;
    }

    public void ResumeGame()
    {
        // Hide the pause menu
        pauseMenuUI.SetActive(false);

        // Resume the game by setting the time scale back to 1
        Time.timeScale = 1f;
    }

    void ReturnToMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");

        // Ensure the game is running by setting the time scale to 1
        Time.timeScale = 1f;
    }

    void QuitGame()
    {
        // Quit the application (only works in standalone builds)
        Application.Quit();
    }
}
