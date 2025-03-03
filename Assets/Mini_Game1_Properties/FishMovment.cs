using UnityEngine;
using System.Collections;

public class FishMovement : MonoBehaviour
{
    public float moveSpeed = 0.5f;           // Speed of horizontal movement
    public float moveDistance = 10f;        // How far the fish will move before disappearing

    public float minX = -5f, maxX = 5f;
    public float minY = -3f, maxY = 3f;

    private Vector3 startPosition;

    void Start()
    {
        RespawnFish();
        StartCoroutine(MoveRight());
    }

    public IEnumerator MoveRight()
    {
        Vector3 initialPosition = transform.position;

        // Move the fish horizontally to the right
        while (Vector3.Distance(initialPosition, transform.position) < moveDistance)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    void RespawnFish()
    {
        // Set the initial position randomly
        startPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
        transform.position = startPosition;
    }
}
