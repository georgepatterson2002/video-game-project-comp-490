using UnityEngine;

public class BasketballShoot : MonoBehaviour
{
    public float shotPower = 10f;
    public float maxShotDistance = 5f;
    public GameObject basketballPrefab;
    private Rigidbody2D rb;
    private bool isShooting = false;
    private GameObject currentBasketball;

    void Start()
    {
        SpawnNewBall();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isShooting = true;
        }

        if (isShooting && currentBasketball != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 shootingDirection = (mousePosition - (Vector2)currentBasketball.transform.position).normalized;
            float distance = Vector2.Distance(mousePosition, (Vector2)currentBasketball.transform.position);
            currentBasketball.GetComponent<Rigidbody2D>().velocity = shootingDirection * Mathf.Min(distance, maxShotDistance) * shotPower;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isShooting = false;
            SpawnNewBall();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(currentBasketball);
        }
    }

    void SpawnNewBall()
    {
        currentBasketball = Instantiate(basketballPrefab, transform.position, Quaternion.identity);
    }
}
