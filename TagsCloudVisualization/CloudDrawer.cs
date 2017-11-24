using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
	public class CloudDrawer
	{
		public static Bitmap DrawMap(IEnumerable<WordInRect> words) {
			var mainRect = LayoutNormalizer.GetMainRect(words);
			var normalizedWords = LayoutNormalizer.ShiftLayout(words, mainRect);
			var bitmap = new Bitmap(mainRect.Width, mainRect.Height);
			var graphics = Graphics.FromImage(bitmap);

			foreach (var word in normalizedWords) {
				graphics.DrawString(word.Word, word.Font, Brushes.Magenta, word.Rect, StringFormat.GenericTypographic);
			}
			return bitmap;
		}
	}
}