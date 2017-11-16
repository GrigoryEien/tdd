using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;

namespace TagsCloudVisualization
{
    public class CloudSaver
    {
        private readonly Point center;

        public CloudSaver(Point center)
        {
            this.center = center;
        }

	    public List<WordInRect> ShiftLayout(List<WordInRect> words, Rectangle mainRect)
	    {
		    var offsetX = -mainRect.X;
		    var offsetY = -mainRect.Y;
		    return words.Select(x =>
		    {
			    x.rect.X += offsetX;
			    x.rect.Y += offsetY;
			    return x;
			    
		    }).ToList();

	    }

	    public IEnumerable<WordInRect> CalculateRectsForWords(Dictionary<string, int> words)
	    {
			var layouter = new CircularCloudLayouter(center);
		    var graphics = Graphics.FromImage(new Bitmap(1, 1));

		    foreach (var word in words) {
			    Font font = new Font(FontFamily.GenericSansSerif, word.Value, FontStyle.Regular, GraphicsUnit.Pixel);
			    SizeF size = graphics.MeasureString(word.Key, font);
				var rect = layouter.PutNextRectangle(size.ToSize());
			    yield return new WordInRect(word.Key, rect, font);
		    }
		}

	    public Rectangle GetMainRect(IEnumerable<WordInRect> words)
	    {
		    Rectangle rect = words.First().rect;
		    foreach (var word in words)
		    {
			    rect = Rectangle.Union(rect, word.rect);
		    }
		    return rect;
	    }

        public void SaveBitmapToFile(Dictionary<string, int> words, string filename)
        {
	        var layoutedWords = CalculateRectsForWords(words);
	        var mainRect = GetMainRect(layoutedWords);
	        layoutedWords = ShiftLayout(layoutedWords.ToList(),mainRect);

            var bitmap = new Bitmap(mainRect.Width, mainRect.Height);
            var graphics = Graphics.FromImage(bitmap);

            foreach (var word in layoutedWords)
            {
                graphics.DrawString(word.word, word.font, Brushes.Magenta, word.rect, StringFormat.GenericTypographic);
            }

            bitmap.Save(filename);
            Console.WriteLine("Saved to " + filename);
        }
    }
}