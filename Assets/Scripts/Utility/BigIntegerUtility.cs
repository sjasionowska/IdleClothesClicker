// Some of the concepts taken from https://stackoverflow.com/questions/37907411/c-sharp-format-arbitrarily-large-biginteger-for-endless-game
// question from Thijs Riezebeek stackoverlow user. To learn more about this class check 
// https: //github.com/sjasionowska/BigIntegerFormatter.

using System.Collections.Generic;
using System.Numerics;

/// <summary>
/// Static class used to format the BigIntegers.
/// </summary>
public static class BigIntegerUtility
{
	private static List<string> suffixes = new List<string>();

	/// <summary>
	/// If it's equal to 0, there are only suffixes from an empty string to Q on the suffixes list.
	/// If it's equal to 1, there are a - z suffixes added.
	/// If it's equal to 2, there are aa - zz suffixes added and so on.
	/// </summary>
	private static int suffixesCounterForGeneration = 0;

	/// <summary>
	/// Formats BigInteger using engineering notation - with a suffix. Returns a number without the
	/// suffix if the length of the number is smaller than 4.
	/// </summary>
	/// <param name="number">Number to format.</param>
	/// <returns>Returns string that contains BigInteger formatted using engineering notation.</returns>
	public static string FormatWithSuffix(BigInteger number)
	{
		return FormatNumberWithSuffixString(number.ToString());
	}

	private static string FormatNumberWithSuffixString(string numberAsString)
	{
		// if number length is smaller than 4, just returns the number
		if (numberAsString.Length < 4) return numberAsString;

		// Counts scientific exponent. This will be used to determine which suffix from the 
		// suffixes List should be used. 
		var exponentIndex = numberAsString.Length - 1;

		// Digits before a comma. Can be one, two or three of them - that depends on the exponentsIndex.
		var leadingDigit = "";

		// Digits after a comma. Always three of them or less, if the formatted number will have zero 
		// on its end.
		var decimals = "";

		// Example: if the number the methods is formatting is 12345, exponentsIndex is 4, 4 % 3 = 1. 
		// There will be two leading digits. There will be three decimals. Formatted number will look like:
		// 12.345k
		switch (exponentIndex % 3)
		{
			case 0:
				leadingDigit = numberAsString.Substring(0, 1);
				decimals = numberAsString.Substring(1, 3);
				break;

			case 1:
				leadingDigit = numberAsString.Substring(0, 2);
				decimals = numberAsString.Substring(2, 3);
				break;

			case 2:
				leadingDigit = numberAsString.Substring(0, 3);
				decimals = numberAsString.Substring(3, 3);
				break;
		}

		// Trims zeros from the number's end.
		var numberWithoutSuffix = $"{leadingDigit}.{decimals}";
		numberWithoutSuffix = numberWithoutSuffix.TrimEnd('0').TrimEnd('.');

		var suffix = GetSuffixForNumber(exponentIndex / 3);

		// Returns number in engineering format.
		// return $"{numberWithoutSuffix}{suffixes[exponentIndex / 3]}";

		return $"{numberWithoutSuffix}{suffix}";
	}

	/// <summary>
	/// Gets suffix under a given index which is actually a number of thousands.
	/// </summary>
	/// <param name="suffixIndex">Suffix index. Number of thousands.</param>
	/// <returns>Suffix under a given index - suffix for a given number of thousands.</returns>
	private static string GetSuffixForNumber(int suffixIndex)
	{
		// Creates initial suffixes List with an empty string, k, M, B and Q
		if (suffixes.Count == 0) suffixes = CreateSuffixesList();

		// Fills the suffixes list if there's a need to
		if (suffixes.Count - 1 < suffixIndex) FillSuffixesList(suffixes, suffixIndex);

		return suffixes[suffixIndex];
	}

	private static List<string> CreateSuffixesList()
	{
		var suffixesList = new List<string>
		{
			"", "k", "M", "B", "Q"
		};

		return suffixesList;
	}

	private static void FillSuffixesList(List<string> suffixesList, int suffixIndex)
	{
		// while the suffixes list length - 1 is smaller than the suffix index of the suffix that we need
		// (e.g.: when there's a need for an 'a' suffix:
		// when suffixesList = "", "k", "M", "B", "Q"
		// suffixesList.Count = 5, suffixIndex for a 'Q' is 4,
		// suffixIndex for an 'a' is 5)
		while (suffixesList.Count - 1 < suffixIndex)
		{
			// happens only once, when suffixList is filled only with 
			// initial values
			if (suffixesCounterForGeneration == 0)
			{
				for (int i = 97; i <= 122; i++)
				{
					// k excluded because of thousands suffix
					if (i == 107) continue;

					// cache the character a - z
					char character = (char)i;
					suffixesList.Add(char.ToString(character));
				}

				suffixesCounterForGeneration++;
			}
			else
			{
				// for every character (a - z) counts how many times the character should be generated as the suffix
				for (var i = 97; i <= 122; i++)
				{
					// cache the character a - z
					char character = (char)i;

					// placeholder for a generated suffix
					string generatedSuffix = "";

					// counts how many times one character should be used as one suffix and adds them
					// basing on the suffixesCounterForGeneration which is the number telling us how many times 
					// the suffixes were generated
					for (var counter = 1; counter <= suffixesCounterForGeneration + 1; counter++)
					{
						generatedSuffix += character.ToString();
					}

					// adds the generated suffix to the suffixes list
					suffixesList.Add(generatedSuffix);
				}

				suffixesCounterForGeneration++;
			}
		}
	}
}
