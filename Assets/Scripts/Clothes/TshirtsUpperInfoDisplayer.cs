/// <summary>
/// Info displayer for Tshirts
/// </summary>
public class TshirtsUpperInfoDisplayer : UpperInfoDisplayer
{
	/// <inheritdoc />
	protected override void RefreshDisplayer(int level)
	{
		levelOnUiText.text = string.Format("Level {0}", NumberUtility.FormatNumber(level, 3));
		infoOnAutomaticProductionText.text = string.Format(
			"{0} {1} / 5 seconds",
			NumberUtility.FormatNumber(level, 3), 
			level == 1 ? "t-shirt" : "t-shirts");
		infoOnPrice.text = string.Format("{0} $ for 1 {1}", NumberUtility.FormatNumber(level, 3), "t-shirt");
	}
}
