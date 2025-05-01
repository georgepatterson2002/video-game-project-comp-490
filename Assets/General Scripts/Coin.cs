using UnityEngine;


public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    public float hoverSpeed = 2f;
    public float hoverHeight = 0.2f;


    private Vector3 startPosition;


    private void Start()
    {
        startPosition = transform.position;
    }


    private void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && GameManager.instance != null)
        {
            GameManager.instance.AddCoins(coinValue);
            GameManager.instance.SaveCoins();        


            if (CurrencyHUD.instance != null)
            {
                CurrencyHUD.instance.UpdateCurrencyDisplay();
            }


            Destroy(gameObject);
        }
    }
}
