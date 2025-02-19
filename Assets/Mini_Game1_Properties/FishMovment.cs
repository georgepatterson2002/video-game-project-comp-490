using UnityEngine;
using System.Collections;

public class FishMovement : MonoBehaviour
{
    public float jumpHeight = 2f;        // Jump height
    public float jumpDuration = 2f;      // Time it takes to complete one jump
    public float moveRightAmount = 2f;   // How far the fish moves right after each jump
    public float moveSpeed = 1f;         // Speed of horizontal movement
    public float minX = -5f, maxX = 5f;  // Random X range for respawn
    public float minY = -3f, maxY = 3f;  // Random Y range for respawn
    public float delayBetweenJumps = 1f; // Delay before next jump

    private Vector3 startPosition;
    private float jumpTimer;
    private bool isJumping = true;  // Controls when the fish jumps

    void Start()
    {
        RespawnFish();  // Set initial position
        StartCoroutine(JumpCycle()); // Start the jumping loop
    }

    void Update()
    {
        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
            float jumpProgress = Mathf.PingPong(jumpTimer, jumpDuration) / jumpDuration;
            transform.position = startPosition + Vector3.up * (jumpHeight * Mathf.Sin(jumpProgress * Mathf.PI));
        }
    }

    IEnumerator JumpCycle()
    {
        while (true)
        {
            // Wait for jump to complete
            yield return new WaitForSeconds(jumpDuration);

            // Move slightly to the right over time (instead of instantly)
            float moveTime = 0f;
            Vector3 initialPosition = startPosition;
            Vector3 targetPosition = startPosition + Vector3.right * moveRightAmount;

            while (moveTime < moveRightAmount / moveSpeed)
            {
                moveTime += Time.deltaTime;
                startPosition = Vector3.Lerp(initialPosition, targetPosition, moveTime / (moveRightAmount / moveSpeed));
                yield return null;
            }

            // Respawn the fish if it moves too far right
            if (startPosition.x > maxX)
            {
                RespawnFish();
            }

            isJumping = false; // Stop jumping
            yield return new WaitForSeconds(delayBetweenJumps); // Wait before next jump
            isJumping = true;  // Start jumping again
        }
    }

    void RespawnFish()
    {
        startPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), startPosition.z);
        jumpTimer = 0f;
    }
}
