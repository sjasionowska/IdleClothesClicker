using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyAmount : MonoBehaviour
{
    public Text moneyUiText;
    private static int amount;

    private void Awake()
    {
        TshirtManager.Sold += AddMoney;
        moneyUiText.text = amount.ToString();
    }

    private void OnDestroy()
    {
        TshirtManager.Sold -= AddMoney;
    }

    public void AddMoney(int moneyToAdd)
    {
        amount += moneyToAdd;
    }
}
