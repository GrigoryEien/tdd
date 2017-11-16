using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{
	public class DictionaryNormalizer
	{
		public static Dictionary<string, int> NormalizeDictionary(Dictionary<string, int> dict) {
			var ratio = 100.0 / dict.Values.Max();
			var normalizedList = dict.ToDictionary(x => x.Key, x => ConvertValue((int)(x.Value * ratio)));
			return normalizedList;
		}

		private static int ConvertValue(int value) {
			var OldRange = (100 - 0);
			var NewRange = (100 - 10);
			return (((value - 0) * NewRange) / OldRange) + 10;
		}
	}
}