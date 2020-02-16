using System;
using System.Collections.Generic;

using UnityEngine;

public class Store : MonoBehaviour
{
	/// <summary>
	/// Action invoked when some money is earned with parameter sending amount of earned money
	/// </summary>
	public event Action<int> MoneyEarned;

	/// <summary>
	/// Action invoked when something was bought and money is spent.
	/// </summary>
	public event Action<int> MoneySpent;

	/// <summary>
	/// instance created so we can call non-static method in a delegate
	/// </summary>
	private static Store instance;

	[SerializeField]
	private List<Clothes> clothes;

	private Action<int, int> myDelegate = delegate(int amount, int price) { instance.EarnMoney(amount, price); };

	private void Awake()
	{
		Debug.LogFormat("{0} on Awake.", this);

		instance = this;
		foreach (var item in clothes)
		{
			item.SoldAll += myDelegate;
			item.UpgradeBought += ItemOnUpgradeBought;
		}
	}

	private void OnDestroy()
	{
		foreach (var item in clothes)
		{
			item.SoldAll -= myDelegate;
		}
	}

	private void EarnMoney(int amount, int price)
	{
		var moneyEarned = amount * price;
		MoneyEarned?.Invoke(moneyEarned);
	}

	private void SpendMoney(int moneySpent)
	{
		MoneySpent?.Invoke(moneySpent);
		Debug.Log("MoneySpent " + moneySpent);
	}

	private void ItemOnUpgradeBought(int moneySpent)
	{
		SpendMoney(moneySpent);
	}
}
