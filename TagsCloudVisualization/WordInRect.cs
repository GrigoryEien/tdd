using System.Drawing;

namespace TagsCloudVisualization
{
	public class WordInRect
	{
		public string word;
		public Rectangle rect;
		public Font font;

		public WordInRect(string word, Rectangle rect, Font font)
		{
			this.word = word;
			this.rect = rect;
			this.font = font;
		}
	}
}