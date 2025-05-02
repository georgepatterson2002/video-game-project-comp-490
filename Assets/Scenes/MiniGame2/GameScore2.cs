
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;

    public Text scoreText;
    public Text coinText;
    public Text highScoreText;
    public Text statusText;
    public int miniGameNumber = 2;

    private float lastScoreTime = -Mathf.Infinity;
    public float scoreCooldown = 0.75f;

    void Start()
    {
        UpdateAllUI();
    }

    public void AddPoint()
    {
        if (Time.time - lastScoreTime >= scoreCooldown)
        {
            score += 10;
            lastScoreTime = Time.time;
            UpdateScoreText();

            if (GameManager.instance != null)
            {
                GameManager.instance.AddCoins(10); // Local only
                UpdateCoinText();

                int currentHigh = GetCurrentHighScore();
                if (score > currentHigh)
                {
                    SetNewHighScore(score); // Local only
                    UpdateHighScoreText();
                }
            }
        }
    }

    public void SaveAndExit()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.SaveCoins(); // Save coins to Supabase
            GameManager.instance.SaveMiniGameScoreIfHigher(miniGameNumber, score); // Save high score to Supabase

            if (statusText != null)
                statusText.text = "Progress saved!";

            SceneManager.LoadScene("MainMap"); // Exit to main map
        }
    }

    private void UpdateAllUI()
    {
        UpdateScoreText();
        UpdateCoinText();
        UpdateHighScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "SCORE: " + score;
    }

    private void UpdateCoinText()
    {
        if (coinText != null && GameManager.instance != null)
            coinText.text = "COINS: " + GameManager.instance.coins;
    }

    private void UpdateHighScoreText()
    {
        if (highScoreText != null && GameManager.instance != null)
            highScoreText.text = "HIGH SCORE: " + GetCurrentHighScore();
    }

    private int GetCurrentHighScore()
    {
        switch (miniGameNumber)
        {
            case 1: return GameManager.instance.highScoreMiniGame1;
            case 2: return GameManager.instance.highScoreMiniGame2;
            case 3: return GameManager.instance.highScoreMiniGame3;
            default: return 0;
        }
    }

    private void SetNewHighScore(int newScore)
    {
        switch (miniGameNumber)
        {
            case 1: GameManager.instance.highScoreMiniGame1 = newScore; break;
            case 2: GameManager.instance.highScoreMiniGame2 = newScore; break;
            case 3: GameManager.instance.highScoreMiniGame3 = newScore; break;
        }
    }
}