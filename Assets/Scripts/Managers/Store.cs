using System;
using System.Collections.Generic;

using UnityEngine;

public class Store : MonoBehaviour
{
	/// <summary>
	/// instance created so we can call non-static method in a delegate
	/// </summary>
	private static Store instance;

	[SerializeField]
	private List<Clothes> clothes;

	/// <summary>
	/// Action invoked when some money is earned with parameter sending amount of earned money
	/// </summary>
	public event Action<int> MoneyEarned;

	private Action<int, int> myDelegate = delegate(int amount, int price) { instance.EarnMoney(amount, price); };

	private void Awake()
	{
		instance = this;
		foreach (var item in clothes)
		{
			item.Made += ItemOnMade;
			item.SoldAll += myDelegate;
		}
	}

	private void EarnMoney(int amount, int price)
	{
		var moneyEarned = amount * price;
		MoneyEarned?.Invoke(moneyEarned);
	}

	private void OnDestroy()
	{
		foreach (var item in clothes)
		{
			item.Made -= ItemOnMade;
			item.SoldAll -= myDelegate;
		}
	}

	private void ItemOnMade(int obj)
	{
		
	}
}
