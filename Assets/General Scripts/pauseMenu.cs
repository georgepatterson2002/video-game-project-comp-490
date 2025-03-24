using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject player;

    private bool isPaused = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        if (pauseMenuUI == null)
        {
            Debug.LogWarning("PauseMenuUI not assigned in inspector!");
            return;
        }

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

       
    }

    public void ResumeGame()
    {
        if (pauseMenuUI == null)
        {
            Debug.LogWarning("PauseMenuUI not assigned in inspector!");
            return;
        }

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

       
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;

        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }

        // Destroy this PauseMenu so the main menu scene doesn't have leftovers
        Destroy(gameObject);

        SceneManager.LoadScene("MainMenu");
    }
    public void HomeMainMenu(){
        Time.timeScale =1f;
        isPaused = false;
        if(pauseMenuUI != null){
            pauseMenuUI.SetActive(false);
        }
         Destroy(gameObject);

        SceneManager.LoadScene("MainMap");
    }
    

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);

        Time.timeScale = 1f;
        isPaused = false;

        // Re-assign player if there's one in the new scene
        player = GameObject.FindWithTag("Player");

        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
    }

    public void SetPauseMenuUI(GameObject panel)
    {
        pauseMenuUI = panel;
        ResumeGame();
    }
}
