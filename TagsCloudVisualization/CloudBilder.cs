using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
	public static class CloudBilder
	{
		public static WordInRect[] CalculateRectsForWords(Dictionary<string, int> words, Point center) {
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