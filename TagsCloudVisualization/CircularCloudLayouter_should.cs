using NUnit.Framework;
using System.Drawing;


namespace TagsCloudVisualization {
    [TestFixture]
    public class CircularCloudLayouter_should {
        private CircularCloudLayouter layouter;
        [SetUp]
        public void SetUp() {
            layouter = new CircularCloudLayouter(new Point(20, 20));
        }

        [Test]
        public void CreateNewLayouter_WhenPointIsPassed() {
            layouter = new CircularCloudLayouter(new Point(20, 20));
        }
        [Test]
        public void ReturnRectangle_WhenPassedSize() {
            var size = new Size(20,20);

            var supposedlyRect = layouter.PutNextRectangle(size);
            
            Assert.IsInstanceOf<Rectangle>(supposedlyRect);
        }

        [Test]
        public void AvoidRectangleIntersection_WhenPuttingSecondRectangle()
        {
            var size = new Size(20,20);
            var firstRect = layouter.PutNextRectangle(size);
            var secondRect = layouter.PutNextRectangle(size);

            Assert.False(firstRect.IntersectsWith(secondRect));
        }

    }
}