using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // Value of the coin
    public float hoverSpeed = 1f; // Speed of the hovering motion
    public float hoverHeight = 0.2f; // Height of the hovering motion

    private Vector3 startPosition;

    private void Start()
    {
        // Record the starting position of the coin
        startPosition = transform.position;
    }

    private void Update()
    {
        // Make the coin hover up and down
        float newY = startPosition.y + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CurrencyHUD currencyHUD = FindObjectOfType<CurrencyHUD>();
            if (currencyHUD != null)
            {
                currencyHUD.AddCurrency(coinValue);
            }

            Destroy(gameObject);
        }
    }
}
