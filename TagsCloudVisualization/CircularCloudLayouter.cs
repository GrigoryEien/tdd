﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
	public class CircularCloudLayouter
	{
		private Point center;
		private const int HorizontalExtensionCoeff = 2;
		private const int SpiralEnlargementAngle = 125;
		private readonly List<Rectangle> rectangles;
		private double angle;


		public CircularCloudLayouter(Point center)
		{
			this.center = center;
			rectangles = new List<Rectangle>();
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
			return newRect;
		}


		private static Rectangle MoveRectToItsCenter(Rectangle rect)
		{
			rect.X -= rect.Size.Width / 2;
			rect.Y -= rect.Size.Height / 2;
			return rect;
		}

		private Point CalculateFermatSpiral(Point offset)
		{
			angle += SpiralEnlargementAngle;
			var x = (int) (Math.Sqrt(angle * HorizontalExtensionCoeff) * Math.Cos(angle));
			var y = (int) (Math.Sqrt(angle) * Math.Sin(angle));
			return new Point(x + offset.X, y + offset.Y);
		}

		private bool IntestectsWithOther(Rectangle rect)
		{
			return rectangles.Any((r) => r.IntersectsWith(rect));
		}

	}
}