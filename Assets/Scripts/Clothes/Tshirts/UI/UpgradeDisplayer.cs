using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used for displaying upgrade information.
/// </summary>
public class UpgradeDisplayer : MonoBehaviour
{
#pragma warning disable 0649
	[SerializeField]
	private Clothes clothes;
#pragma warning restore 0649

#pragma warning disable 0649
	[SerializeField]
	private Text priceText;
#pragma warning restore 0649

	private void Awake()
	{
		clothes.LevelIncreased += RefreshPrice;
	}

	private void Start()
	{
		RefreshPrice();
	}

	private void OnDestroy()
	{
		clothes.LevelIncreased -= RefreshPrice;
	}

	private void RefreshPrice()
	{
		priceText.text = NumberUtility.FormatNumber(clothes.MoneyNeededToUpgrade, 3) + " $";

		// priceText.text = clothes.MoneyNeededToUpgrade + "$";
	}
}
