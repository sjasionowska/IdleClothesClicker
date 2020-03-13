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
		var speed = level / productionSpeedInv;

		levelOnUiText.text = string.Format("Level {0}", NumberUtility.FormatNumber(level, 3));
		infoOnAutomaticProductionText.text = string.Format(
			"{0} {1} / second",
			NumberUtility.FormatNumber(speed, 3),
			// ReSharper disable once CompareOfFloatsByEqualityOperator
			speed == 1f ? "t-shirt" : "t-shirts");
		infoOnPrice.text = string.Format("{0} $ for 1 {1}", NumberUtility.FormatNumber(level, 3), "t-shirt");
	}
}
