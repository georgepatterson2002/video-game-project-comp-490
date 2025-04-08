using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{

    [SerializeField] private int _value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CurrencyHUD.instance.AddCurrency(1);
            Destroy(gameObject);
        }
    }
}
