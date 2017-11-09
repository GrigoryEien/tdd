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

        private List<Rectangle> rectangles;
        private Rectangle mainRectangle;
        private Rectangle container;
        private double angle;


        public CircularCloudLayouter(Point center)
        {
            this.center = center;
            rectangles = new List<Rectangle>();
            container = new Rectangle();
        }

        public Rectangle PutNextRectangle(Size size)
        {
//            var point = new Point(mainRectangle.Right,center.Y);
//                         
//            var newRect = new Rectangle(point,size);
//            rectangles.Add (new Rectangle(center, size));
//
//
//            mainRectangle = mainRectangle == null? newRect: Rectangle.Union(mainRectangle,newRect);
            var position = CalculateFermatSpiral( center);
            var newRect = MoveRectToItsCenter(new Rectangle(position, size));
            while (intestectsWithOther(newRect))
            {
                 position = CalculateFermatSpiral( center);
                newRect = MoveRectToItsCenter(new Rectangle(position, size));
                Console.WriteLine(angle);
            }
            rectangles.Add(newRect);
            return newRect;
        }

//        private Rectangle PutRectangleIntoRandomEmptyPosition(Rectangle rect)
//        {
//            
//        }

        private Rectangle MoveRectToItsCenter(Rectangle rect)
        {
            rect.X -= rect.Size.Width / 2;
            rect.Y -= rect.Size.Height / 2;
            return rect;
        }
        private Point CalculateFermatSpiral(Point offset) {
            angle += 250;
            var x = (int)(Math.Sqrt(angle) * Math.Cos(angle));
            var y = (int)(Math.Sqrt(angle) * Math.Sin(angle));
            return new Point(x + offset.X, y + offset.Y);
        }

        private bool intestectsWithOther(Rectangle rect)
        {
            return rectangles.Any((r) => r.IntersectsWith(rect));
        }
    }
}