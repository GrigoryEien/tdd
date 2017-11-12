using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;

namespace TagsCloudVisualization
{
    public class CloudVisualizer
    {
        private readonly Point center;

        public CloudVisualizer(Point center)
        {
            this.center = center;
        }

        public void SaveBitmapToFile(Dictionary<string, int> words, string filename)
        {
            var layouter = new CircularCloudLayouter(center);
            var bitmap = new Bitmap(1600, 1600);
            var graphics = Graphics.FromImage(bitmap);

            foreach (var word in words)
            {
                Font font = new Font(FontFamily.GenericSansSerif, word.Value, FontStyle.Regular, GraphicsUnit.Pixel);

                SizeF size = graphics.MeasureString(word.Key, font);
                Console.WriteLine(word.Key);
                Console.WriteLine(size);

                var rect = layouter.PutNextRectangle(size.ToSize());
                graphics.DrawString(word.Key, font, Brushes.Magenta, rect, StringFormat.GenericTypographic);
            }

            bitmap.Save(filename);
            Console.WriteLine("Saved!");
        }
    }
}