using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public static GameManagement instance;

    [SerializeField] private GameObject _gameOverCanvas;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        Time.timeScale = 1f;
    }
    public void GameOver(int finalScore)
    {
        PlayerPrefs.SetInt("FinalScore", finalScore);
        PlayerPrefs.SetInt("MiniGameNumber", 3);

        if (GameManager.instance != null)
        {
            GameManager.instance.SaveMiniGameScoreIfHigher(3, finalScore);
            GameManager.instance.SaveCoins();
        }

        SceneManager.LoadScene("YouLost3");
        Debug.Log($"[GameManagement] Game OverScore: {finalScore}, MiniGame: 3");
    }
}
