using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplayer : MonoBehaviour
{
	[SerializeField]
	private Text moneyAmountOnUiText;
	
	private MoneyManager moneyManager;

	private int amount => moneyManager.Amount;

	private void Awake()
	{		Debug.LogFormat("{0} on Awake.", this);

		moneyManager = FindObjectOfType<MoneyManager>();
		if(moneyAmountOnUiText != null) moneyAmountOnUiText.text = amount.ToString();
		else Debug.LogError("moneyAmountOnUiText is null!");
		moneyManager.AmountChanged += RefreshMoneyAmount;

	}

	private void OnDestroy()
	{
		moneyManager.AmountChanged -= RefreshMoneyAmount;
	}

	public void RefreshMoneyAmount()
	{
		if(moneyAmountOnUiText != null) moneyAmountOnUiText.text = amount.ToString();
		else Debug.LogError("moneyAmountOnUiText is null!");		
	}
}
