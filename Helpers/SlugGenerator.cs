using System.Collections.Generic;
using System.Text;

namespace Lawyer.Helpers
{
	public static class SlugGenerator
	{
		private static readonly Dictionary<char, string> _charBinding;

		static SlugGenerator()
		{
			_charBinding = new Dictionary<char, string>()
			{
				['а'] = "a",
				['б'] = "b",
				['в'] = "v",
				['г'] = "g",
				['д'] = "d",
				['е'] = "e",
				['ё'] = "yo",
				['ж'] = "zh",
				['з'] = "z",
				['и'] = "i",
				['й'] = "j",
				['к'] = "k",
				['л'] = "l",
				['м'] = "m",
				['н'] = "n",
				['о'] = "o",
				['п'] = "p",
				['р'] = "r",
				['с'] = "s",
				['т'] = "t",
				['у'] = "u",
				['ф'] = "f",
				['х'] = "x",
				['ц'] = "c",
				['ч'] = "ch",
				['ш'] = "sh",
				['щ'] = "shh",
				['ы'] = "y",
				['ь'] = "'",
				['э'] = "e",
				['ю'] = "yu",
				[' '] = "-",
				['0'] = "0",
				['1'] = "1",
				['2'] = "2",
				['3'] = "3",
				['4'] = "4",
				['5'] = "5",
				['6'] = "6",
				['7'] = "7",
				['8'] = "8",
				['9'] = "9",
			};
		}

		public static string GenerateSlug(string title)
		{
			StringBuilder builder = new StringBuilder();

			for (int i = 0; i < title.Length; i++)
			{
				if (_charBinding.TryGetValue(char.ToLower(title[i]), out string value))
				{
					builder.Append(value);
				}
				else
				{
					builder.Append('-');
				}
			}

			return builder.ToString();
		}
	}
}