using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    [SerializeField] private int _value = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.instance != null)
        {
            GameManager.instance.AddCoins(_value);    
            GameManager.instance.SaveCoins();          

            if (CurrencyHUD.instance != null)
            {
                CurrencyHUD.instance.UpdateCurrencyDisplay();
            }

            Destroy(gameObject);
        }
    }
}
