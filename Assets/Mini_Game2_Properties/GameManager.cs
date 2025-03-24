using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;  // UI Text to display score
    public Text timerText;  // UI Text to display timer
    public GameObject basketball;  // Reference to the basketball object
    private float timer = 10f;  // 10-second timer
    private int score = 0;  // Player's score
    void Update()
    {
        //timer
        timer -= Time.deltaTime;
        if(timer<= 0f){
            timer = 0f;
            ShowScore(); 
        }
        timerText.text = "Time: " + Mathf.Ceil(timer).ToString();
    }

public void AddScore()
    {
        score += 1;  // Increase score by 1
        scoreText.text = "Score: " + score.ToString();  // Update score text
    }

    public void RestartGame(){
        score = 0;
        timer = 10f;
        scoreText.text = "Score: 0";
        timerText.text= "Time: 10";
        basketball.transform.position = new Vector3(0, 1, -5);
    }
    void ShowScore()
    {
        scoreText.text = "Final Score: " + score;
    }
}
