using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;

namespace TagsCloudVisualization
{
	public class CircularCloudLayouter
	{
		private Point center;

		private readonly List<Rectangle> rectangles;
		private Rectangle mainRectangle;
		private double angle;


		public CircularCloudLayouter(Point center)
		{
			this.center = center;
			rectangles = new List<Rectangle>();
		}

		public IEnumerable<WordInRect> CalculateRectsForWords(Dictionary<string, int> words) {
			var layouter = new CircularCloudLayouter(center);
			var graphics = Graphics.FromImage(new Bitmap(1, 1));

			foreach (var word in words) {
				Font font = new Font(FontFamily.GenericSansSerif, word.Value, FontStyle.Regular, GraphicsUnit.Pixel);
				SizeF size = graphics.MeasureString(word.Key, font);
				var rect = layouter.PutNextRectangle(size.ToSize());
				yield return new WordInRect(word.Key, rect, font);
			}
		}

		public Rectangle PutNextRectangle(Size size)
		{
			angle = 0;
			Rectangle newRect;
			do
			{
				var position = CalculateFermatSpiral(center);
				newRect = MoveRectToItsCenter(new Rectangle(position, size));
			} while (IntestectsWithOther(newRect));

			rectangles.Add(newRect);
			UpdateMainRect(newRect);
			return newRect;
		}


		private Rectangle MoveRectToItsCenter(Rectangle rect)
		{
			rect.X -= rect.Size.Width / 2;
			rect.Y -= rect.Size.Height / 2;
			return rect;
		}

		private Point CalculateFermatSpiral(Point offset)
		{
			angle += 125;
			var x = (int) (Math.Sqrt(angle * 2) * Math.Cos(angle));
			var y = (int) (Math.Sqrt(angle) * Math.Sin(angle));
			return new Point(x + offset.X, y + offset.Y);
		}

		private bool IntestectsWithOther(Rectangle rect)
		{
			return rectangles.Any((r) => r.IntersectsWith(rect));
		}

		private void UpdateMainRect(Rectangle rect)
		{
			mainRectangle = Rectangle.Union(mainRectangle,rect); 
		}
	}
}