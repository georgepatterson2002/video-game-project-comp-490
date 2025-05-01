using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameScore : MonoBehaviour
{
    public Text scoreText;
    public Text livesText;

    private int score = 0;
    private int lives = 3;

    public int miniGameNumber = 1;

    void Start()
    {
        UpdateScoreText();
        UpdateLivesText();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    public void LoseLife(int amount)
    {
        lives -= amount;
        UpdateLivesText();

        if (lives <= 0)
        {
            PlayerPrefs.SetInt("FinalScore", score);
            PlayerPrefs.SetInt("MiniGameNumber", miniGameNumber);
            
            if (GameManager.instance != null)
            {
                GameManager.instance.SaveMiniGameScoreIfHigher(miniGameNumber, score);


                GameManager.instance.SaveCoins(); 
            }

            SceneManager.LoadScene("YouLost");
            Debug.Log($"Game Over! Score: {score} | Mini-game: {miniGameNumber}");
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    void UpdateLivesText()
    {
        livesText.text = "Lives: " + lives;
    }
}
