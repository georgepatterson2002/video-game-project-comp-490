using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class lostScript3 : MonoBehaviour
{
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;

    private int score;
    private int miniGameNumber;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        score = PlayerPrefs.GetInt("FinalScore", 0);
        miniGameNumber = PlayerPrefs.GetInt("MiniGameNumber", 3);

        if (currentScoreText != null)
            currentScoreText.text = "Score: " + score;

        if (GameManager.instance != null)
        {
            int highScore = 0;
            switch (miniGameNumber)
            {
                case 1: highScore = GameManager.instance.highScoreMiniGame1; break;
                case 2: highScore = GameManager.instance.highScoreMiniGame2; break;
                case 3: highScore = GameManager.instance.highScoreMiniGame3; break;
                default: Debug.LogWarning("Invalid mini-game number"); break;
            }

            if (highScoreText != null)
                highScoreText.text = "High Score: " + highScore;
        }
        else
        {
            Debug.LogWarning("GameManager not found. High score not loaded.");
        }
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("MiniGame3");
        Debug.Log("[lostScript3] Retrying MiniGame3...");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToMainMap()
    {
        SceneManager.LoadScene("MainMap");
    }
}
