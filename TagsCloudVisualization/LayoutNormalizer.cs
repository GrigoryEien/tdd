using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
	public class LayoutNormalizer
	{
		public static WordInRect[] ShiftLayout(IEnumerable<WordInRect> words, Rectangle mainRect) {
			return words.Select(x => {
				x.Rect.X -= mainRect.X;
				x.Rect.Y -= mainRect.Y;
				return x;
			}).ToArray();
		}

		public static Rectangle GetMainRect(IEnumerable<WordInRect> words) {
			return words.Select(x => x.Rect).Aggregate(Rectangle.Union);
		}
	}
}