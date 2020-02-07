using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class MoneyAmount : MonoBehaviour
{
	public Text moneyUiText;

	private static int amount;

	private Store store;

	private void Awake()
	{
		store = FindObjectOfType<Store>();
		store.AllTshirtsSold += AddMoney;
		moneyUiText.text = amount.ToString();
	}

	private void OnDestroy()
	{
		store.AllTshirtsSold -= AddMoney;
	}

	public void AddMoney(int amountOfMadeItems, int amountToAdd)
	{
		AddMoney(amountToAdd);
	}

	public void AddMoney(int moneyToAdd)
	{
		amount += moneyToAdd;
		moneyUiText.text = amount.ToString();
	}
}
