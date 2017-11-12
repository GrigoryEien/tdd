using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;

namespace TagsCloudVisualization {
    public class CircularCloudLayouter
    {
        private Point center;

        private List<Rectangle> rectangles;
        private Rectangle mainRectangle;
        private double angle;


        public CircularCloudLayouter(Point center)
        {
            this.center = center;
            rectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size size)
        {
            angle = 0;
            var position = CalculateFermatSpiral(center);
            
            var newRect = MoveRectToItsCenter(new Rectangle(position, size));
            while (intestectsWithOther(newRect))
            {
                position = CalculateFermatSpiral(center);
                newRect = MoveRectToItsCenter(new Rectangle(position, size));
            }
            rectangles.Add(newRect);
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
            var x = (int) (Math.Sqrt(angle*2) * Math.Cos(angle));
            var y = (int) (Math.Sqrt(angle) * Math.Sin(angle));
            return new Point(x + offset.X, y + offset.Y);
        }

        private bool intestectsWithOther(Rectangle rect)
        {
            return rectangles.Any((r) => r.IntersectsWith(rect));
        }
       

        
        
    }
}
    