using System;

using UnityEngine;
using UnityEngine.UI;

public class TshirtAmount : MonoBehaviour
{
	public UnityEngine.UI.Text itemAmountUiText;

	private int amount;

	private void Awake()
	{
		TshirtManager.Made += AddItemAmount;
	}

	private void OnDestroy()
	{
		TshirtManager.Made -= AddItemAmount;
	}

	public void AddItemAmount(int amountToAdd)
	{
		amount += amountToAdd;
		if (itemAmountUiText != null) 
			itemAmountUiText.text = amount.ToString();
	}
}
