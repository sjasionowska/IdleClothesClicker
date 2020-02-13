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

	private int amount => moneyManager.Amount;

	private void Awake()
	{
		moneyManager = FindObjectOfType<MoneyManager>();
		if(moneyAmountOnUiText != null) moneyAmountOnUiText.text = amount.ToString();
		else Debug.LogError("moneyAmountOnUiText is null!");

		
	}

	public void AddMoney(int moneyToAdd)
	{
		if(moneyAmountOnUiText != null) moneyAmountOnUiText.text = amount.ToString();
		else Debug.LogError("moneyAmountOnUiText is null!");		
	}
}
