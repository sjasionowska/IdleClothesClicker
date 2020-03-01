using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplayer : MonoBehaviour
{
	[SerializeField]
	private Text moneyAmountOnUiText;

	[SerializeField]
	private MoneyManager moneyManager;

	private int Amount => moneyManager.Amount;

	private void Awake()
	{
		Debug.LogFormat("{0} on Awake.", this);
		RefreshMoneyAmount();
		// if (moneyAmountOnUiText != null) moneyAmountOnUiText.text = Amount.ToString();
		// else Debug.LogError("moneyAmountOnUiText is null!");
		moneyManager.AmountChanged += RefreshMoneyAmount;
	}

	private void OnDestroy()
	{
		moneyManager.AmountChanged -= RefreshMoneyAmount;
	}

	private void RefreshMoneyAmount()
	{
		if (moneyAmountOnUiText != null) moneyAmountOnUiText.text = NumberUtility.FormatNumber(Amount, 3) + " $";
		else Debug.LogError("moneyAmountOnUiText is null!");
	}
}
