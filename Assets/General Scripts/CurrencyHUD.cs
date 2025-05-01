using UnityEngine;
using TMPro;


public class CurrencyHUD : MonoBehaviour
{
    public static CurrencyHUD instance;


    public TextMeshProUGUI currencyText;


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


    public void UpdateCurrencyDisplay()
    {
        if (currencyText != null && GameManager.instance != null)
        {
            currencyText.text = GameManager.instance.coins.ToString();
        }
    }
}
