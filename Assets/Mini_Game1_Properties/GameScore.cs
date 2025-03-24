using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameScore : MonoBehaviour
{
    public Text scoreText;
    public Text livesText; // Added: UI text to display lives
    
    private int score = 0;
    private int lives = 3; // Added: starting lives
    
    void Start()
    {
        UpdateScoreText(); // Initialize the score display
        UpdateLivesText(); // Initialize the lives display
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText(); // Moved to a separate function for reuse
    }

    public void LoseLife(int amount)
    {
        lives -= amount;
        UpdateLivesText();

        if (lives <= 0)
        {
            SceneManager.LoadScene("YouLost");
            Debug.Log("Game Over!");

            // You can add game over logic here later
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
