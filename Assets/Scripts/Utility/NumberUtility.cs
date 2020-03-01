public static class NumberUtility
{
	private static int currentMoneyExponentCounter;

	private static string currentMoneyExponent;

	private static string[] shortExponents =
		{"", "k", "M", "B", "T", "Q", "aa", "bb", "cc", "dd", "ee", "ff", "gg", "hh", "ii"};

	public static void DecreaseCurrentMoneyExponentCounter()
	{
		currentMoneyExponentCounter--;
	}

	
	public static void IncreaseCurrentMoneyExponentCounter()
	{
		currentMoneyExponentCounter++;
	}

	public static string FormatNumber(double number, int digits)
	{
		int exponentsCounter = 0;

		while (number / 1000 > 1)
		{
			exponentsCounter++;
			if (exponentsCounter >= shortExponents.Length) exponentsCounter = 0;
			number /= 1000;
		}

		string numberAsString;

		switch (digits)
		{
			case 3:
				numberAsString = string.Format("{0:F3}", number).Contains(".")
					? string.Format("{0:F3}", number).TrimEnd('0').TrimEnd('.')
					: string.Format("{0:F3}", number);
				break;

			case 2:
				numberAsString = string.Format("{0:F2}", number).Contains(".")
					? string.Format("{0:F2}", number).TrimEnd('0').TrimEnd('.')
					: string.Format("{0:F2}", number);
				break;

			case 1:
				numberAsString = string.Format("{0:F1}", number).Contains(".")
					? string.Format("{0:F1}", number).TrimEnd('0').TrimEnd('.')
					: string.Format("{0:F1}", number);
				break;

			default:
				numberAsString = string.Format("{0:F0}", number).Contains(".")
					? string.Format("{0:F0}", number).TrimEnd('0').TrimEnd('.')
					: string.Format("{0:F0}", number);
				break;
		}

		currentMoneyExponentCounter = exponentsCounter;
		currentMoneyExponent = shortExponents[exponentsCounter];
		return numberAsString + shortExponents[exponentsCounter];
	}
}
