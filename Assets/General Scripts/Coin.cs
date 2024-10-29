using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // Value of the coin

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Coin collected!")
            CurrencyHUD currencyHUD = FindObjectOfType<CurrencyHUD>();
            if (currencyHUD != null)
            {
                currencyHUD.AddCurrency(coinValue);
            }

            Destroy(gameObject);
        }
    }
}
