using UnityEngine;
using TMPro;

public class CurrencyHUD : MonoBehaviour
{
    public static CurrencyHUD instance;

    public int currencyAmount = 0; // Initial amount of currency
    public TextMeshProUGUI currencyText; // Reference to the Text component

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        UpdateCurrencyDisplay();
    }

    public void AddCurrency(int amount)
    {
        currencyAmount += amount;
        UpdateCurrencyDisplay();
    }

    public void SubtractCurrency(int amount)
    {
        currencyAmount -= amount;
        UpdateCurrencyDisplay();
    }

    private void UpdateCurrencyDisplay()
    {
        currencyText.text = currencyAmount.ToString();
    }

}
