using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;

    private float lastScoreTime = -Mathf.Infinity;
    public float scoreCooldown = 0.75f; // in seconds

    void Start()
    {
        UpdateScoreText();
    }

    public void AddPoint()
    {
        // Only allow scoring if cooldown has passed
        if (Time.time - lastScoreTime >= scoreCooldown)
        {
            score+= 10;
            lastScoreTime = Time.time;
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "SCORE: " + score.ToString();
    }
}
