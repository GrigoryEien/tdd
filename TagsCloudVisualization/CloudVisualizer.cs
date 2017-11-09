using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;

namespace TagsCloudVisualization
{
    public class CloudVisualizer
    {
        Random rnd;
        private Point center;
        public CloudVisualizer(Point center)
        {
            rnd = new Random();
            this.center = center;

        }
        public void DrawBitmap()
        {
            var layouter = new CircularCloudLayouter(center);
            var bitmap = new Bitmap(400,400);
            var graphics = Graphics.FromImage(bitmap);

//            for (var i = 0; i < 50; i++)
//            {
//                graphics.DrawRectangle(new Pen(Color.Cyan), layouter.PutNextRectangle(GenerateRandomSize()));
//            }

            foreach (var randomSize in GenerateSortedListOfRandomSizes(50))
            {
                graphics.DrawRectangle(new Pen(Color.Cyan), layouter.PutNextRectangle(randomSize));
            }

            bitmap.Save("savedTest.bmp");
            Console.WriteLine("Saved!");
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