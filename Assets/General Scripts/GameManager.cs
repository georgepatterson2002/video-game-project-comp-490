using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Login Session")]
    public string accessToken;
    public string userId;

    [Header("Player Data")]
    public int coins;
    public int highScoreMiniGame1;
    public int highScoreMiniGame2;
    public int highScoreMiniGame3;

    public TextMeshProUGUI coinDisplay;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitializeSession(string token, string uid)
    {
        accessToken = token;
        userId = uid;
        StartCoroutine(EnsurePlayerDataExists());
    }

    public void SetCoinText(TextMeshProUGUI coinText)
    {
        coinDisplay = coinText;
        UpdateCoinDisplay();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinDisplay();
    }

    public void SaveCoins()
    {
        StartCoroutine(SaveCoinsToDatabase());
    }

    public void Logout()
    {
        accessToken = null;
        userId = null;
        coins = 0;
        highScoreMiniGame1 = 0;
        highScoreMiniGame2 = 0;
        highScoreMiniGame3 = 0;
        SceneManager.LoadScene("LoginScene");
        Debug.LogError("request error");
    }

    private void UpdateCoinDisplay()
    {
        if (coinDisplay != null)
            coinDisplay.text = coins.ToString();
    }

    private IEnumerator EnsurePlayerDataExists()
    {
        string url = $"{SupabaseConfig.Url}/rest/v1/player_data?user_id=eq.{userId}&select=coins,high_score_mini_game_1,high_score_mini_game_2,high_score_mini_game_3";
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("apikey", SupabaseConfig.ApiKey);
        request.SetRequestHeader("Authorization", "Bearer " + accessToken);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            var wrapper = JsonUtility.FromJson<PlayerDataArray>("{\"items\":" + json + "}");

            if (wrapper.items.Length > 0)
            {
                var data = wrapper.items[0];
                coins = data.coins;
                highScoreMiniGame1 = data.high_score_mini_game_1;
                highScoreMiniGame2 = data.high_score_mini_game_2;
                highScoreMiniGame3 = data.high_score_mini_game_3;
                UpdateCoinDisplay();
            }
            else
            {
                Debug.Log("[GameManager] No existing player_data found. Creating...");
                yield return StartCoroutine(CreatePlayerData());
            }
        }
        else
        {
            Debug.LogError("Failed to load player data: " + request.error);
            Debug.LogError("Response: " + request.downloadHandler.text);
        }
    }

    private IEnumerator CreatePlayerData()
    {
        string url = $"{SupabaseConfig.Url}/rest/v1/player_data";
        string jsonBody = $@"{{
            ""user_id"": ""{userId}"",
            ""coins"": 0,
            ""high_score_mini_game_1"": 0,
            ""high_score_mini_game_2"": 0,
            ""high_score_mini_game_3"": 0
        }}";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("apikey", SupabaseConfig.ApiKey);
        request.SetRequestHeader("Authorization", $"Bearer {accessToken}");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to create player data: " + request.error);
            Debug.LogError("Response: " + request.downloadHandler.text);
        }
        else
        {
            Debug.Log("[GameManager] Created new player_data row.");
            coins = 0;
            highScoreMiniGame1 = 0;
            highScoreMiniGame2 = 0;
            highScoreMiniGame3 = 0;
            UpdateCoinDisplay();
        }
    }

    private IEnumerator SaveCoinsToDatabase()
    {
        string url = $"{SupabaseConfig.Url}/rest/v1/player_data?user_id=eq.{userId}";
        string jsonBody = $"{{\"coins\": {coins} }}";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

        UnityWebRequest request = UnityWebRequest.Put(url, bodyRaw);
        request.method = "PATCH";
        request.SetRequestHeader("apikey", SupabaseConfig.ApiKey);
        request.SetRequestHeader("Authorization", $"Bearer {accessToken}");
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Prefer", "return=representation");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to save coins: " + request.error);
            Debug.LogError("Response: " + request.downloadHandler.text);
        }
        else
        {
            Debug.Log("[GameManager] Coins saved successfully.");
        }
    }

    public void SaveMiniGameScoreIfHigher(int gameNumber, int newScore)
    {
        int currentScore = 0;

        switch (gameNumber)
        {
            case 1: currentScore = highScoreMiniGame1; break;
            case 2: currentScore = highScoreMiniGame2; break;
            case 3: currentScore = highScoreMiniGame3; break;
            default:
                Debug.LogWarning("Unsupported mini-game number: " + gameNumber);
                return;
        }

        if (newScore > currentScore)
        {
            StartCoroutine(SaveScoreCoroutine(gameNumber, newScore));
        }
        else
        {
            Debug.Log($"[GameManager] Score {newScore} not higher than existing score {currentScore} for game {gameNumber}");
        }
    }

    private IEnumerator SaveScoreCoroutine(int gameNumber, int score)
    {
        string field = $"high_score_mini_game_{gameNumber}";
        string url = $"{SupabaseConfig.Url}/rest/v1/player_data?user_id=eq.{userId}";
        string jsonBody = $"{{ \"{field}\": {score} }}";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

        UnityWebRequest request = UnityWebRequest.Put(url, bodyRaw);
        request.method = "PATCH";
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("apikey", SupabaseConfig.ApiKey);
        request.SetRequestHeader("Authorization", $"Bearer {accessToken}");
        request.SetRequestHeader("Prefer", "resolution=merge-duplicates");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Failed to save mini-game score: {request.error}");
            Debug.LogError("Response: " + request.downloadHandler.text);
        }
        else
        {
            Debug.Log($"[GameManager] Saved high score for mini-game {gameNumber}: {score}");

            // Update local copy
            if (gameNumber == 1) highScoreMiniGame1 = score;
            else if (gameNumber == 2) highScoreMiniGame2 = score;
            else if (gameNumber == 3) highScoreMiniGame3 = score;
        }
    }

    [System.Serializable]
    public class PlayerData
    {
        public int coins;
        public int high_score_mini_game_1;
        public int high_score_mini_game_2;
        public int high_score_mini_game_3;
    }

    [System.Serializable]
    public class PlayerDataArray
    {
        public PlayerData[] items;
    }
}
