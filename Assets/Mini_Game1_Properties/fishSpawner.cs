using UnityEngine;
using UnityEngine.UI;  // Use TMPro instead if needed

public class FishSpawner : MonoBehaviour
{
    public GameObject FishPrefab;  // Good Fish Prefab
    public GameObject badFishPrefab;   // Bad Fish Prefab
    public GameObject gun;
    public GameObject player;
    public float spawnDelay = 2f;

    public Vector2 spawnAreaMin = new Vector2(-5f, -3f);
    public Vector2 spawnAreaMax = new Vector2(5f, 3f);

    public Text countdownText;  // Drag your UI Text (or TMP_Text)
    public Image countdownFrame;

    public bool countdownActive = true;

    void Start()
    {

        StartCoroutine(StartCountdown());
        gun.gameObject.SetActive(false);
    }

    System.Collections.IEnumerator StartCountdown()
    {
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

            yield return new WaitForSecondsRealtime(1f);

            countdownTime -= 1f;
        }

        if (countdownText != null)
        {
            countdownText.text = "Go!";
            yield return new WaitForSecondsRealtime(1f);
            countdownText.gameObject.SetActive(false);
            countdownFrame.gameObject.SetActive(false);
        }

        Time.timeScale = 1f;
        countdownActive = false;
        gun.gameObject.SetActive(true);

        InvokeRepeating("SpawnFish", 0f, spawnDelay);
    }

    void SpawnFish()
    {
        if (countdownActive) return;

        Vector3 spawnPosition = new Vector3(Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                                            Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                                            -1f);

        // Randomly choose between good fish and bad fish (50/50 chance)
        GameObject fishToSpawn = (Random.value < 0.5f) ? FishPrefab : badFishPrefab;

        Instantiate(fishToSpawn, spawnPosition, Quaternion.identity);
    }
}
