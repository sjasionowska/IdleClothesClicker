using System;
using System.Numerics;

using UnityEngine;

public class MoneyManager : MonoBehaviour
{
	/// <summary>
	/// Invoked when Amount of money changes
	/// </summary>
	public event Action AmountChanged;

	private Store store;

	private BigInteger amount;

	/// <summary>
	/// Amount of money owned
	/// </summary>
	public BigInteger Amount
	{
		get => amount;
		private set
		{
			if (value < 0)
				Debug.LogErrorFormat(
					"You're trying to set money amount that is less than zero. Omg, what a shame :facepalm: : '{0}",
					value);
			else amount = value;
		}
	}

	private void Awake()
	{
		Debug.LogFormat("{0} on Awake.", this);

		// Amount = 2147483640;
		Amount = new BigInteger(0);

		store = FindObjectOfType<Store>();
		store.MoneyEarned += EarnMoney;
		store.MoneySpent += SpendMoney;
	}

	private void OnDestroy()
	{
		store.MoneyEarned -= EarnMoney;
		store.MoneySpent -= SpendMoney;
	}

	private void EarnMoney(int moneyEarned)
	{
		Amount += moneyEarned;
		Debug.LogFormat("Money amount owned: {0}", Amount);
		AmountChanged?.Invoke();
	}

	private void SpendMoney(int moneySpent)
	{
		Amount -= moneySpent;
		Debug.LogFormat("Money amount owned: {0}", Amount);
		AmountChanged?.Invoke();
	}
}
