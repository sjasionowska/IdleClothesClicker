using System;

using UnityEngine;
using UnityEngine.UI;

public class TshirtAmount : MonoBehaviour
{
	public UnityEngine.UI.Text itemAmountUiText;

	private int amount;

	public int Amount
	{
		get => amount;
		private set => amount = value;
	}

	private Store manager;

	private void Awake()
	{
		manager = FindObjectOfType<Store>();
		manager.TshirtMade += AddAmountOfTshirts;
		manager.AllTshirtsSold += RemoveSoldAllTshirtsItems;
		if (itemAmountUiText != null) itemAmountUiText.text = Amount.ToString();
	}

	private void OnDestroy()
	{
		manager.TshirtMade -= AddAmountOfTshirts;
		manager.AllTshirtsSold -= RemoveSoldAllTshirtsItems;
	}

	public void AddAmountOfTshirts(int amountToAdd)
	{
		Amount += amountToAdd;
		if (itemAmountUiText != null) itemAmountUiText.text = Amount.ToString();
	}

	public void RemoveSoldAllTshirtsItems(int tshirtAmountToRemove, int earned)
	{
		RemoveSoldItems(tshirtAmountToRemove);
	}

	public void RemoveSoldItems(int tshirtAmountToRemove)
	{
		if (Amount < tshirtAmountToRemove) Amount = 0;
		Amount -= tshirtAmountToRemove;
		if (itemAmountUiText != null) itemAmountUiText.text = Amount.ToString();
	}
}
