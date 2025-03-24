using UnityEngine;

public class BasketballBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basket"))
        {
            Destroy(gameObject); // Disappear on basket score
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            Destroy(gameObject); // Disappear on hitting the floor
        }
    }
}
