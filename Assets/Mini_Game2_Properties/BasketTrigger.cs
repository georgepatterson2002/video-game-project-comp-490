using UnityEngine;

public class BasketTrigger : MonoBehaviour
{
    public GameManager gameManager;  // Reference to GameManager

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball"))  // Check if it’s the basketball
        {
            gameManager.AddScore();  // Add score
            other.transform.position = new Vector3(0, 1, -5);  // Reset ball position
        }
    }
}
