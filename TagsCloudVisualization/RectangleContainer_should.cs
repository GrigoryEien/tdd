using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class RectangleContainer_should
    {
        private RectanglesContainer rectanglesContainer;

        [SetUp]
        public void SetUp()
        {
            rectanglesContainer = new RectanglesContainer(new Rectangle(100,100,50,50));    
        }

        [Test]
        public void ShouldUpdateBorders_WhenNewRectangleIsAddedToContainer()
        {
            var first_rect = new Rectangle(100,100,50,50);
            var rect = new Rectangle(100,80,50,50);
            var expectedUpdatedRect = new Rectangle(100,80,50,70);
            Rectangle.Union(first_rect, rect).Should().Be(expectedUpdatedRect);
            // rectanglesContainer.Update(rect);
             first_rect = new Rectangle(0, 0, 50, 50);
             rect = new Rectangle(100, 100, 50, 50);
             expectedUpdatedRect = new Rectangle(0, 0, 150, 150);
            Rectangle.Union(first_rect, rect).Should().Be(expectedUpdatedRect);

        }
        
    }
}