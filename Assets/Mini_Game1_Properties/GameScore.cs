using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    public Text scoreText; 
    private int score = 0;  

 
    public void AddScore(int amount)
    {
        score += amount; 
        scoreText.text = "Score: " + score; 
    }
}
