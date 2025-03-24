using UnityEngine;
using UnityEngine.UI;  // Use TMPro instead if needed

public class FishSpawner : MonoBehaviour
{
    public GameObject fishPrefab;
    public float spawnDelay = 2f;

    public Vector2 spawnAreaMin = new Vector2(-5f, -3f);
    public Vector2 spawnAreaMax = new Vector2(5f, 3f);

    public Text countdownText;  // Drag your UI Text (or TMP_Text)
    public GameObject player;   // Drag your Player here (optional, for disabling shooting/movement)

    private bool countdownActive = true;

    void Start()
    {
        // Start the countdown process
        StartCoroutine(StartCountdown());
    }

    System.Collections.IEnumerator StartCountdown()
    {
        // Freeze everything
        Time.timeScale = 0f;

        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
        }


        float countdownTime = 3f;

        while (countdownTime > 0)
        {
            if (countdownText != null)
                countdownText.text = Mathf.Ceil(countdownTime).ToString();

            // Wait in unscaled time because we froze Time.timeScale
            yield return new WaitForSecondsRealtime(1f);

            countdownTime -= 1f;
        }

        // Show "Go!" for a moment
        if (countdownText != null)
        {
            countdownText.text = "Go!";
            yield return new WaitForSecondsRealtime(1f);
            countdownText.gameObject.SetActive(false);
        }

        // Resume everything
        Time.timeScale = 1f;
        countdownActive = false;

        // Start spawning fish after countdown
        InvokeRepeating("SpawnFish", 0f, spawnDelay);
    }

    void SpawnFish()
    {
        if (countdownActive) return;  // Safety check

        Vector3 spawnPosition = new Vector3(Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                                            Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                                            -1f);

        Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
    }
}
