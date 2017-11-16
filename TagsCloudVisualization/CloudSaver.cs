using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;

namespace TagsCloudVisualization
{
	public static class CloudSaver
	{
		public static List<WordInRect> ShiftLayout(List<WordInRect> words, Rectangle mainRect)
		{
			var offsetX = -mainRect.X;
			var offsetY = -mainRect.Y;
			return words.Select(x =>
			{
				x.Rect.X += offsetX;
				x.Rect.Y += offsetY;
				return x;
			}).ToList();
		}


		public static Rectangle GetMainRect(IEnumerable<WordInRect> words)
		{
			Rectangle rect = words.First().Rect;
			foreach (var word in words)
			{
				rect = Rectangle.Union(rect, word.Rect);
			}
			return rect;
		}

		public static void SaveBitmapToFile(IEnumerable<WordInRect> words, string filename)
		{
			var mainRect = GetMainRect(words);
			words = ShiftLayout(words.ToList(), mainRect);

			var bitmap = new Bitmap(mainRect.Width, mainRect.Height);
			var graphics = Graphics.FromImage(bitmap);

			foreach (var word in words)
			{
				graphics.DrawString(word.Word, word.Font, Brushes.Magenta, word.Rect, StringFormat.GenericTypographic);
			}

			bitmap.Save(filename);
			Console.WriteLine("Saved to " + filename);
		}
	}
}