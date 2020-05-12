using System.Numerics;

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used for displaying money amount on the UI.
/// </summary>
public class MoneyDisplayer : MonoBehaviour
{
#pragma warning disable 0649
	[SerializeField]
	private Text moneyAmountOnUiText;
#pragma warning restore 0649

#pragma warning disable 0649
	[SerializeField]
	private MoneyManager moneyManager;
#pragma warning restore 0649

	private BigInteger Amount => moneyManager.Amount;

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
		if (moneyAmountOnUiText != null) moneyAmountOnUiText.text = BigIntegerUtility.FormatWithSuffix(Amount) + " $";
		else Debug.LogError("moneyAmountOnUiText is null!");
	}
}
