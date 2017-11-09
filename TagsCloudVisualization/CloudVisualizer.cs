using System;
using System.Drawing;

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

            for (var i = 0; i < 30; i++)
            {
                graphics.DrawRectangle(new Pen(Color.Cyan), layouter.PutNextRectangle(GenerateRandomSize()));
            }

            bitmap.Save("savedTest.bmp");
            Console.WriteLine("Saved!");
        }

        private Size GenerateRandomSize()
        {
            return new Size(rnd.Next(40,100),rnd.Next(10,20));
        }
    }
}