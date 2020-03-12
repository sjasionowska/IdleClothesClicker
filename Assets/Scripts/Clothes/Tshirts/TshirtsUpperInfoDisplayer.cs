/// <summary>
/// Info displayer for Tshirts
/// </summary>
public class TshirtsUpperInfoDisplayer : UpperInfoDisplayer
{
	/// <inheritdoc />
	protected override void RefreshDisplayer()
	{
		var level = clothes.Level;
		var productionSpeedInv = clothes.ProductionSpeedInversed;

		levelOnUiText.text = string.Format("Level {0}", NumberUtility.FormatNumber(level, 3));
		infoOnAutomaticProductionText.text = string.Format(
			"{0} {1} / {2} seconds",
			NumberUtility.FormatNumber(level, 3),
			level == 1 ? "t-shirt" : "t-shirts",
			NumberUtility.FormatNumber(productionSpeedInv, 3));
		infoOnPrice.text = string.Format("{0} $ for 1 {1}", NumberUtility.FormatNumber(level, 3), "t-shirt");
	}
}
