using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Camera mainCamera;
    public GameScore gameScore; // Removed the extra text here

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left Click to shoot
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero); // Raycast to detect what the mouse is pointing at

            if (hit.collider != null)
            {
                // If the ray hits an object with the "Fish" tag, add points to the score
                if (hit.collider.CompareTag("Fish"))
                {
                    // Add 10 points to the score when hitting a fish
                    gameScore.AddScore(10); 

                    Destroy(hit.collider.gameObject); // Destroy the fish
                }
                else
                {
                    // Message when the shot misses (optional)
                    Debug.Log("Shot missed! No fish hit.");
                }
            }
        }
    }
}
