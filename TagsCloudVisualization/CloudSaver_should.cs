using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
	public class CloudSaver_should
	{
		[TestFixture]
		public class CloudSaver_should_Should
		{
			[Test]
			public void CalculateRectangleUnion_WhenPassedRectangleList()
			{
				var rects = new List<Rectangle>()
				{
					new Rectangle(0, 0, 10, 10),
					new Rectangle(-10, -10, 10, 10),
					new Rectangle(10, 0, 10, 10)
				};
				Font font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular, GraphicsUnit.Pixel);
				var wordsInRects = rects.Select(x => new WordInRect("test", x, font)).ToList();
				var expectedRect = new Rectangle(-10, -10, 30, 20);
				var actualRect = CloudSaver.GetMainRect(wordsInRects);
				expectedRect.Should().Be(actualRect);
			}

			[Test]
			public void ShiftLayout_WhenMainRectangleIsPassed()
			{
				var rects = new List<Rectangle>()
				{
					new Rectangle(0, 0, 10, 10),
					new Rectangle(-10, -10, 10, 10),
					new Rectangle(10, 0, 10, 10)
				};
				var mainRectangle = new Rectangle(-10, -10, 30, 20);
				var expectedRects = new List<Rectangle>()
				{
					new Rectangle(10, 10, 10, 10),
					new Rectangle(0, 0, 10, 10),
					new Rectangle(20, 10, 10, 10)
				};
				var font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular, GraphicsUnit.Pixel);
				var expectedWordsInRects = expectedRects.Select(x => new WordInRect("test", x, font)).ToList();
				var wordsInRects = rects.Select(x => new WordInRect("test", x, font)).ToList();

				var actualRects = CloudSaver.ShiftLayout(wordsInRects, mainRectangle);

				actualRects.ShouldBeEquivalentTo(expectedWordsInRects);
			}
		}
	}
}