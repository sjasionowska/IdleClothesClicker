using UnityEngine;
using UnityEngine.UI;

// ReSharper disable UseStringInterpolation

public class UpperInfoDisplayer : MonoBehaviour
{
	/// <summary>
	/// Info on which Clothes info should this displayer display
	/// </summary>
	[SerializeField]
	protected Clothes clothes;

	/// <summary>
	/// Text with info about level
	/// </summary>
	[SerializeField]
	protected Text levelOnUiText;

	/// <summary>
	/// Text with info about automatic production at this moment
	/// </summary>
	[SerializeField]
	protected Text infoOnAutomaticProductionText;

	/// <summary>
	/// Text with info about price for one item of Clothes
	/// </summary>
	[SerializeField]
	protected Text infoOnPrice;

	private void Awake()
	{
		clothes.LevelIncreased += RefreshDisplayer;
		clothes.ProductionSpeedIncreased += RefreshDisplayer;
	}

	private void OnDestroy()
	{
		clothes.LevelIncreased -= RefreshDisplayer;
		clothes.ProductionSpeedIncreased -= RefreshDisplayer;
	}

	/// <summary>
	/// Refreshes info about given Clothes. Virtual so names of Clothes can be overriden,
	/// especially for plural and singular forms.
	/// </summary>
	protected virtual void RefreshDisplayer()
	{
		var level = clothes.Level;
		var productionSpeedInv = clothes.ProductionSpeedInversed;
		var speed = level / productionSpeedInv;

		levelOnUiText.text = string.Format("Level {0}", NumberUtility.FormatNumber(level, 3));
		infoOnAutomaticProductionText.text = string.Format(
			"{0} {1} / second",
			NumberUtility.FormatNumber(speed, 3),
			clothes.name);
		infoOnPrice.text = string.Format("{0} $ for 1 {1}", NumberUtility.FormatNumber(level, 3), clothes.name);
	}
}
