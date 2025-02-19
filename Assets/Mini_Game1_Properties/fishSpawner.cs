using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject fishPrefab;  // Reference to the fish prefab
    public float spawnDelay = 2f;  // Delay between spawning fish
    public Vector2 spawnAreaMin = new Vector2(-5f, -3f); // Minimum spawn position
    public Vector2 spawnAreaMax = new Vector2(5f, 3f);  // Maximum spawn position

    void Start()
    {
        // Start spawning fish repeatedly with a delay
        InvokeRepeating("SpawnFish", 0f, spawnDelay);
    }

    void SpawnFish()
    {
        // Random position within the spawn area
        Vector2 spawnPosition = new Vector2(Random.Range(spawnAreaMin.x, spawnAreaMax.x), 
                                             Random.Range(spawnAreaMin.y, spawnAreaMax.y));

        // Instantiate the fish at the random position
        Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
    }
}
