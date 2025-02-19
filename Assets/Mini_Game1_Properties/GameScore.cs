using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    public Text scoreText;  // Reference to the UI Text to display the score
    private int score = 0;  // The current score

    // Method to add score
    public void AddScore(int amount)
    {
        score += amount;  // Add the specified amount to the score
        scoreText.text = "Score: " + score;  // Update the UI with the new score
    }
}
