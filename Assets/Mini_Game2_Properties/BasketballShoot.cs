using UnityEngine;

public class BasketballShoot : MonoBehaviour
{
    public GameObject basketballPrefab;
    private GameObject currentBall;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentBall == null)
        {
            Vector2 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentBall = Instantiate(basketballPrefab, spawnPos, Quaternion.identity);
        }
    }

    public void NotifyBallDestroyed()
    {
        currentBall = null;
        SpawnNewBall();
    }

    void SpawnNewBall()
    {
        Vector2 spawnPos = new Vector2(-5, -2); 
        currentBall = Instantiate(basketballPrefab, spawnPos, Quaternion.identity);
    }
}
