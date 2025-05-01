using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;
using SimpleJSON;

public class SupabaseAuth : MonoBehaviour
{
    public string email;
    public string password;
    public string username;
    public TextMeshProUGUI statusText;

    public void SignUp() => StartCoroutine(SignUpRequest());
    public void Login() => StartCoroutine(LoginRequest());

    private void SetStatus(string message)
    {
        Debug.Log(message);
        if (statusText != null)
            statusText.text = message;
    }

    IEnumerator SignUpRequest()
    {
        SetStatus("Creating your account...");

        string json = $"{{\"email\":\"{email}\",\"password\":\"{password}\"}}";
        var request = new UnityWebRequest(SupabaseConfig.Url + "/auth/v1/signup", "POST");
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("apikey", SupabaseConfig.ApiKey);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            var errorJson = JSON.Parse(request.downloadHandler.text);
            string msg = errorJson["msg"] ?? request.error;
            SetStatus("Signup failed: " + msg);
            yield break;
        }

        var responseJson = JSON.Parse(request.downloadHandler.text);

        if (responseJson["user"].IsNull)
        {
            SetStatus("An account with this email already exists. Please log in instead.");
            yield break;
        }

        SetStatus("Account created! Logging in...");
        yield return StartCoroutine(LoginAfterSignup());
    }

    IEnumerator LoginAfterSignup()
    {
        yield return StartCoroutine(LoginRequest(true));
    }

    IEnumerator LoginRequest(bool isFromSignup = false)
    {
        SetStatus(isFromSignup ? "Auto-logging in..." : "Logging in...");

        string json = $"{{\"email\":\"{email}\",\"password\":\"{password}\"}}";
        var request = new UnityWebRequest(SupabaseConfig.Url + "/auth/v1/token?grant_type=password", "POST");
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("apikey", SupabaseConfig.ApiKey);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            var errorJson = JSON.Parse(request.downloadHandler.text);
            string msg = errorJson["msg"] ?? request.error;
            SetStatus("Login failed: " + msg);
            yield break;
        }

        var resultJson = JSON.Parse(request.downloadHandler.text);
        string accessToken = resultJson["access_token"];
        string userId = resultJson["user"]["id"];

        if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(userId))
        {
            SetStatus("Login failed: Missing session info.");
            yield break;
        }

        GameManager.instance?.InitializeSession(accessToken, userId);

        if (isFromSignup)
        {
            SetStatus("Checking for existing user data...");
            bool hasData = false;
            yield return StartCoroutine(CheckIfUserDataExists(userId, accessToken, exists => hasData = exists));

            if (hasData)
            {
                SetStatus("Account already exists. Please log in instead.");
                yield break;
            }

            SetStatus("Setting up your account...");
            yield return StartCoroutine(CreateInitialUserData(userId, accessToken, username));
        }

        SetStatus("Loading your data...");
        yield return StartCoroutine(LoadPlayerData(userId, accessToken));

        SetStatus("Login complete!");
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator CheckIfUserDataExists(string userId, string token, System.Action<bool> callback)
    {
        string url = $"{SupabaseConfig.Url}/rest/v1/player_data?user_id=eq.{userId}&select=user_id";
        var request = UnityWebRequest.Get(url);
        request.SetRequestHeader("apikey", SupabaseConfig.ApiKey);
        request.SetRequestHeader("Authorization", "Bearer " + token);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            var dataArray = JSON.Parse(request.downloadHandler.text).AsArray;
            callback(dataArray.Count > 0);
        }
        else
        {
            Debug.LogError("Error checking existing data: " + request.error);
            callback(true); 
        }
    }

    IEnumerator CreateInitialUserData(string userId, string token, string username)
    {
        string profileJson = $"{{\"id\":\"{userId}\",\"username\":\"{username}\"}}";

        string playerJson = $@"{{
            ""user_id"":""{userId}"",
            ""coins"":0,
            ""high_score_mini_game_1"":0,
            ""high_score_mini_game_2"":0,
            ""high_score_mini_game_3"":0
        }}";

        yield return UpsertRequest("/rest/v1/profiles", profileJson, token, "id");
        yield return UpsertRequest("/rest/v1/player_data", playerJson, token, "user_id");
    }

    IEnumerator LoadPlayerData(string userId, string token)
    {
        string url = $"{SupabaseConfig.Url}/rest/v1/player_data?user_id=eq.{userId}&select=coins,high_score_mini_game_1,high_score_mini_game_2,high_score_mini_game_3";
        var request = UnityWebRequest.Get(url);
        request.SetRequestHeader("apikey", SupabaseConfig.ApiKey);
        request.SetRequestHeader("Authorization", "Bearer " + token);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            SetStatus("Failed to load data: " + request.error);
        }
        else
        {
            var dataArray = JSON.Parse(request.downloadHandler.text).AsArray;
            if (dataArray.Count > 0)
            {
                var data = dataArray[0];
                GameManager.instance.coins = data["coins"];
                GameManager.instance.highScoreMiniGame1 = data["high_score_mini_game_1"];
                GameManager.instance.highScoreMiniGame2 = data["high_score_mini_game_2"];
                GameManager.instance.highScoreMiniGame3 = data["high_score_mini_game_3"];
                GameManager.instance.SaveCoins(); 
            }
            else
            {
                SetStatus("No player data found, creating defaults...");
                yield return StartCoroutine(CreateInitialUserData(userId, token, username));
            }
        }
    }

    IEnumerator UpsertRequest(string path, string json, string token, string conflictColumn)
    {
        var request = new UnityWebRequest($"{SupabaseConfig.Url}{path}?on_conflict={conflictColumn}", "POST");
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("apikey", SupabaseConfig.ApiKey);
        request.SetRequestHeader("Authorization", "Bearer " + token);
        request.SetRequestHeader("Prefer", "resolution=merge-duplicates");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"UPSERT failed to {path}: {request.error}");
            Debug.LogError("Response: " + request.downloadHandler.text);
        }
    }
}
