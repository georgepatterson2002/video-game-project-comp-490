using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause Menu UI Panel")]
    public GameObject pauseMenuUI;

    private bool isPaused = false;

    void Update()
    {
        // Check for Escape key to toggle pause/resume
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    // Call this to pause the game
    public void PauseGame()
    {
        if (pauseMenuUI == null)
        {
            Debug.LogWarning("PauseMenuUI not assigned in inspector!");
            return;
        }

        pauseMenuUI.SetActive(true);   // Show pause menu
        Time.timeScale = 0f;           // Pauses game time
        isPaused = true;
    }

    // Call this to resume the game
    public void ResumeGame()
    {
        if (pauseMenuUI == null)
        {
            Debug.LogWarning("PauseMenuUI not assigned in inspector!");
            return;
        }

        pauseMenuUI.SetActive(false);  // Hide pause menu
        Time.timeScale = 1f;           // Resumes game time
        isPaused = false;
    }

    // Go back to the main menu
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");  // Hardcode the scene name here
    }

    // Quit the game
    public void QuitGame()
    {
        Application.Quit();
    }

    // Link the specific pause menu UI panel (called when switching scenes)
    public void SetPauseMenuUI(GameObject panel)
    {
        pauseMenuUI = panel;
        ResumeGame(); // Ensure game isn't paused when switching scenes
    }
}
