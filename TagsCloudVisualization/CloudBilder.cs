using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
	public static class CloudBilder
	{
		public static Bitmap BuildCloud(IEnumerable<string> lines,int count)
		{
			var frequentWords = FrequencyAnalyzer.GetFrequencyDict(lines);
			var mostFrequentWords = frequentWords
				.OrderByDescending(x => x.Value)
				.Take(count)
				.ToDictionary(x => x.Key, x => x.Value);
			mostFrequentWords = DictionaryNormalizer.NormalizeDictionary(mostFrequentWords);
			var rects = CloudBilder.CalculateRectsForWords(mostFrequentWords, new Point(0, 0));
			return CloudDrawer.DrawMap(rects);
		}
		
		private static WordInRect[] CalculateRectsForWords(Dictionary<string, int> words, Point center) {
			var layouter = new CircularCloudLayouter(center);
			var graphics = Graphics.FromImage(new Bitmap(1, 1));

			return words.Select(x =>
			{
				var font = new Font(FontFamily.GenericSansSerif, x.Value, FontStyle.Regular, GraphicsUnit.Pixel);
				var size = graphics.MeasureString(x.Key, font);
				var rect = layouter.PutNextRectangle(size.ToSize());
				return new WordInRect(x.Key, rect, font);

			}).ToArray();
		}
	}
}