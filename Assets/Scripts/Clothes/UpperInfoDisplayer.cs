using UnityEngine;
using UnityEngine.UI;

public class UpperInfoDisplayer : MonoBehaviour
{
	[SerializeField]
	private Clothes clothes;

	[SerializeField]
	private Text levelOnUiText;

	[SerializeField]
	private Text infoOnAutomaticProductionText;

	[SerializeField]
	private Text infoOnPrice;

	private void Awake()
	{
		clothes.LevelIncreased += RefreshDisplayer;
	}

	private void OnDestroy()
	{
		clothes.LevelIncreased -= RefreshDisplayer;
	}

	private void RefreshDisplayer(int level)
	{
		levelOnUiText.text = string.Format("Level {0}", level.ToString());
		infoOnAutomaticProductionText.text = string.Format("{0} {1} / 5 seconds", level.ToString(), clothes.name);
		infoOnPrice.text = string.Format("{0}$ for 1 {1}", level.ToString(), clothes.name);
	}
}
