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
        private Random rnd;
        private Point center;
        public CloudVisualizer(Point center)
        {
            rnd = new Random();
            this.center = center;

        }
        public void DrawBitmap(Dictionary<string,int> words) {
            var layouter = new CircularCloudLayouter(center);
            var bitmap = new Bitmap(800, 800);
            var graphics = Graphics.FromImage(bitmap);

            foreach (var word in words) {
                Font font = new Font(FontFamily.GenericSansSerif, word.Value, FontStyle.Regular, GraphicsUnit.Point);

                SizeF size = graphics.MeasureString(word.Key, font);
//                size.Width += 20;
                Console.WriteLine(word.Key);
                Console.WriteLine(size);

                var rect = layouter.PutNextRectangle(size.ToSize());
//                graphics.DrawRectangle(new Pen(Color.Cyan), rect);
                graphics.DrawString(word.Key, font, Brushes.Magenta, rect,StringFormat.GenericTypographic);

            }

            bitmap.Save("words_in_rects.bmp");
            Console.WriteLine("Saved!");
        }

        public void DrawBitmap()
        {
            var layouter = new CircularCloudLayouter(center);
            var bitmap = new Bitmap(800,800);
            var graphics = Graphics.FromImage(bitmap);
            
            foreach (var randomSize in GenerateSortedListOfRandomSizes(100))
            {
                graphics.DrawRectangle(new Pen(Color.Magenta,5), layouter.PutNextRectangle(randomSize));
            }

            bitmap.Save("savedTest.bmp");
            Console.WriteLine("Saved!");
        }

        public void DrawBitmapWithWords(Dictionary<string,int> words) {
            var layouter = new CircularCloudLayouter(center);
            var bitmap = new Bitmap(800, 800);
            var graphics = Graphics.FromImage(bitmap);

            foreach (var word in words) {
                var size = new Size(word.Key.Length * word.Value,word.Value);
                Font font = new Font(FontFamily.GenericMonospace, word.Value, FontStyle.Regular, GraphicsUnit.Pixel);
                var rect = layouter.PutNextRectangle(size);
                graphics.DrawString(word.Key,font,Brushes.Magenta,rect);
                graphics.DrawRectangle(new Pen(Color.DarkCyan), rect );
                //graphics.DrawRectangle(new Pen(Color.Magenta, 5), layouter.PutNextRectangle(size));
            }

            bitmap.Save("withWords.bmp");
            Console.WriteLine("Saved!");
        }

        private IEnumerable<Size> GenerateSizesFromFrequencyDict(Dictionary<string, int> words,Graphics g)
        {
            foreach (var word in words) {
                Font font = new Font(FontFamily.GenericMonospace, word.Value, FontStyle.Regular, GraphicsUnit.Pixel);

                SizeF size = g.MeasureString(word.Key,font);
                Console.WriteLine(size);
                yield return size.ToSize();

            }
        }

        private Size GenerateRandomSize()
        {
            return new Size(rnd.Next(40,100),rnd.Next(10,20));
        }

        private List<Size> GenerateSortedListOfRandomSizes(int size)
        {
            var rects = new List<Size>();
            for (var i = 0; i < size; i++)
            {
                rects.Add(GenerateRandomSize());
            }

            return rects.OrderByDescending(x => x.Height * x.Width).ToList();
        }
    }
}