using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : Button
{
	private MoneyManager moneyManager;
	private int OwnedMoneyAmount => moneyManager.Amount;

	private int moneyNeededToUpgrade = 40;
	
	private void Awake()
	{
		Debug.LogFormat("{0} on Awake.", this);
		moneyManager = FindObjectOfType<MoneyManager>();
		CheckIfCanBeBought();
		moneyManager.AmountChanged += CheckIfCanBeBought;
	}

	private void OnDestroy()
	{
		moneyManager.AmountChanged -= CheckIfCanBeBought;
	}

	private void CheckIfCanBeBought()
	{
		if (OwnedMoneyAmount >= moneyNeededToUpgrade) interactable = true;
		else interactable = false;
	}
}
