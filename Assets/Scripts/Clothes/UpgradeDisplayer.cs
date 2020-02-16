using UnityEngine;
using UnityEngine.UI;

public class UpgradeDisplayer : MonoBehaviour
{
	[SerializeField]
	private Clothes clothes;

	[SerializeField]
	private Text priceText;

	private void Awake()
	{
		clothes.LevelIncreased += RefreshPrice;
	}

	private void OnDestroy()
	{
		clothes.LevelIncreased -= RefreshPrice;
	}

	private void RefreshPrice(int level)
	{
		priceText.text = clothes.MoneyNeededToUpgrade + "$";
	}
}
