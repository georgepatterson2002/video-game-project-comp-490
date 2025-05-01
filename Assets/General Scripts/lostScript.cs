using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class lostScript : MonoBehaviour
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
        miniGameNumber = PlayerPrefs.GetInt("MiniGameNumber", 1);

        if (currentScoreText != null)
            currentScoreText.text = "Score: " + score;

        if (GameManager.instance != null)
        {
            GameManager.instance.SaveCoins();
            GameManager.instance.SaveMiniGameScoreIfHigher(miniGameNumber, score);

            // Display locally stored high score
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
        SceneManager.LoadScene($"Mini_Game{miniGameNumber}");
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
