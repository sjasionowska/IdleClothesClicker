using System;

using UnityEngine;

public class MoneyManager : MonoBehaviour
{
	public event Action AmountChanged;
	
	private Store store;

	private int amount;

	/// <summary>
	/// Amount of money owned
	/// </summary>
	public int Amount
	{
		get => amount;
		private set
		{
			amount = value;
			if (amount < 0)
				Debug.LogErrorFormat(
					"You're trying to set money amount that is less than zero. Omg, what a shame :facepalm: : '{0}",
					amount);
		}
	}

	private void Awake()
	{		Debug.LogFormat("{0} on Awake.", this);

		Amount = 0;
		store = FindObjectOfType<Store>();
		store.MoneyEarned += EarnMoney;
	}

	private void OnDestroy()
	{
		store.MoneyEarned -= EarnMoney;
	}

	private void EarnMoney(int moneyEarned)
	{
		Amount += moneyEarned;
		Debug.LogFormat("Money amount owned: {0}", Amount);
		AmountChanged?.Invoke();
	}
}
