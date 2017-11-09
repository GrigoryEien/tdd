using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class RectanglesContainer
    {
        public Rectangle container;

        public RectanglesContainer(Rectangle rect)
        {
            container = rect;
        }

        public void Update(Rectangle rect) {
            container.X = Math.Min(container.X, rect.X);
        }
    }
}