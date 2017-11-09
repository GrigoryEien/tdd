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
            angle = 0;
            var position = CalculateFermatSpiral(center);
            
            var newRect = MoveRectToItsCenter(new Rectangle(position, size));
            while (intestectsWithOther(newRect))
            {
                position = CalculateFermatSpiral(center);
                newRect = MoveRectToItsCenter(new Rectangle(position, size));
            }
//            newRect = MoveRectToCenterUsingVector2(newRect);
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

        private Point CalculateFermatSpiral(Point offset)
        {
            angle += 125;
            var x = (int) (Math.Sqrt(angle*2) * Math.Cos(angle));
            var y = (int) (Math.Sqrt(angle) * Math.Sin(angle));
            return new Point(x + offset.X, y + offset.Y);
        }

        private bool intestectsWithOther(Rectangle rect)
        {
            return rect.IsEmpty || rectangles.Any((r) => r.IntersectsWith(rect));
        }
       

        private Point Normalize(Point vector)
        {
            var length = GetVectorLength(vector);
            return new Point((int) (vector.X / length), (int) (vector.Y / length));
        }

        private Point addVector(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

        private Point substractVector(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }

        private double GetVectorLength(Point vector)
        {
            return Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2));
        }

        private Rectangle MoveRectToCenterUsingVector(Rectangle rect)
        {
            var vector = new Point(center.X - rect.X, center.Y - rect.Y);
            var unitVector = Normalize(vector);
            for (var i = 0; i < 30; i++)
            {
                rect.Location = addVector(rect.Location, unitVector);
                if (intestectsWithOther(rect))
                {
                    rect.Location = substractVector(rect.Location, unitVector);
                    break;
                }

            }
            vector = new Point(0, center.Y - rect.Y);
            unitVector = Normalize(vector);
            for (var i = 0; i < 60; i++)
            {
                rect.Location = addVector(rect.Location, unitVector);
                if (intestectsWithOther(rect))
                {
                    rect.Location = substractVector(rect.Location, unitVector);
                    break;
                }
            }
            vector = new Point(center.X - rect.X, 0);
            unitVector = Normalize(vector);
            for (var i = 0; i < 60; i++)
            {
                rect.Location = addVector(rect.Location, unitVector);
                if (intestectsWithOther(rect))
                {
                    rect.Location = substractVector(rect.Location, unitVector);
                    break;
                }

            }
            return rect;

        }

        private Rectangle MoveRectToCenterUsingVector2(Rectangle rect)
        {
            for (var i = 0; i < 30000; i++) {
                var vector = new Point((center.X - rect.X) / 2 +1, (center.Y - rect.Y) / 2 +1);

                rect.Location = addVector(rect.Location, vector);
                if (intestectsWithOther(rect))
                {
                    rect.Location = substractVector(rect.Location, vector);
                    break;
                }

            }

           

            return rect;

        }
    }
}
    