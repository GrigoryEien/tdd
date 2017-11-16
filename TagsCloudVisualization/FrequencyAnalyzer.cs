using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace TagsCloudVisualization
{
	public class FrequencyAnalyzer
	{
		public static Dictionary<string, int> GetFrequencyDict(IEnumerable<string> lines, int minLength = 4)
		{
			return
				lines.SelectMany(x => x.Split(' '))
					.Select(s => s.ToUpper().Trim(new[] {'.', ',', '\'', '!', '(', ')'}))
					.Where(s => s.Length >= minLength)
					.GroupBy(s => s)
					.ToDictionary(g => g.Key, g => g.Count());
		}
	}
}