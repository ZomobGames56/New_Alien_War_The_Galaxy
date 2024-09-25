using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyScript : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> currencyText;
    private int coin;
    private const string CURRENCY_STRING = "Currency";
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey(CURRENCY_STRING))
        {
            coin = 9000;
            PlayerPrefs.SetInt(CURRENCY_STRING, coin);
            coin = PlayerPrefs.GetInt(CURRENCY_STRING);
            SaveCoin();
        }
        else
        {
            coin = 0;
            SaveCoin();
        }
    }
    private void SaveCoin()
    {
        PlayerPrefs.SetInt(CURRENCY_STRING, coin);
        foreach(TextMeshProUGUI text in currencyText)
        {
            text.text = coin.ToString();
        }
    }
    public void AddCoin(int increment)
    {
        coin += increment;
        SaveCoin();
    }
    public void RemoveCoin(int decrement) {
        coin-= decrement;
        SaveCoin();
    }
    public int GetCoin()
    {
        return coin;
    }
}
