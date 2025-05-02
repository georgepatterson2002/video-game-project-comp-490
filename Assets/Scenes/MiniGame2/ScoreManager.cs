using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour{
    [Header("Score")]
    public int score = 0;
    public Text scoreText;

    [Header("UI References")]
    public Text coinText;
    public Text highScoreText;

    [Header("Settings")]
    public float scoreCooldown = 0.75f; // in seconds

    private float lastScoreTime = -Mathf.Infinity;

    void Start(){
        UpdateScoreText();
        UpdateCoinDisplay();
        UpdateHighScoreDisplay();
    }

    public void AddPoint(){
        if (Time.time - lastScoreTime >= scoreCooldown){
            score += 10;
            lastScoreTime = Time.time;
            UpdateScoreText();

            GameManager.instance.AddCoins(10);
            UpdateCoinDisplay(); 
        }
    }

    void UpdateScoreText(){
        if (scoreText != null)
            scoreText.text = "SCORE: " + score.ToString();
    }

    void UpdateCoinDisplay(){
        if (coinText != null)
            coinText.text = "Coins: " + GameManager.instance.coins;
    }

    void UpdateHighScoreDisplay(){
        if (highScoreText != null)
            highScoreText.text = "High Score: " + GameManager.instance.highScoreMiniGame2;
    }

    public void SaveAndExit(){
        GameManager.instance.SaveMiniGameScoreIfHigher(2, score);
        GameManager.instance.SaveCoins();
        SceneManager.LoadScene("MainMap");
    }
}
