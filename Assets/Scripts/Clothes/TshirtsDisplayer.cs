using System;

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for displaying amount of Tshirts on Scene
/// </summary>
public class TshirtsDisplayer : MonoBehaviour
{
	[SerializeField]
	private Tshirts tshirts;

	[SerializeField]
	private UnityEngine.UI.Text amountOnUiText;

	private int Amount
	{
		get => tshirts.Amount;
	}

	private void Awake()
	{
		tshirts.AmountChanged += Refresh;
		if (amountOnUiText != null) amountOnUiText.text = Amount.ToString();
		else Debug.LogError("amountOnUiText is null!");
	}

	private void OnDestroy()
	{
		tshirts.AmountChanged -= Refresh;
	}

	private void Refresh()
	{
		if (amountOnUiText != null) amountOnUiText.text = Amount.ToString();
		else Debug.LogError("amountOnUiText is null!");
	}
}
