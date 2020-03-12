using System;

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for displaying amount of Tshirts on Scene
/// </summary>
public class TshirtsDisplayer : MonoBehaviour
{
#pragma warning disable 0649
	[SerializeField]
	private Tshirts tshirts;
#pragma warning restore 0649

#pragma warning disable 0649
	[SerializeField]
	private UnityEngine.UI.Text amountOnUiText;
#pragma warning restore 0649

	private int Amount
	{
		get => tshirts.Amount;
	}

	private void Awake()
	{
		Debug.LogFormat("{0} on Awake.", this);

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
