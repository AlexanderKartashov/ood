using NSubstitute;
using NUnit.Framework;
using painter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter.Tests
{
	[TestFixture]
	[Parallelizable]
	public class RectangleTests
	{
		private static Point?[] Values = { null, new Point(0, 0) };

		[Test]
		public void InitTest(
			[ValueSource("Values")] Point? leftTop,
			[ValueSource("Values")] Point? rightBottom)
		{
			if (leftTop != null && rightBottom != null)
			{
				Assert.That(() => new Rectangle(leftTop, rightBottom, Color.Black), Throws.Nothing);
			}
			else
			{
				Assert.That(() => new Rectangle(leftTop, rightBottom, Color.Black), Throws.ArgumentNullException);
			}
		}

		[Test]
		public void DrawTest()
		{
			var canvas = Substitute.For<ICanvas>();
			var leftTop = new Point(10, 20);
			var rightTop = new Point(60, 20);
			var rightBottom = new Point(60, 15);
			var leftBottom = new Point(10, 15);
			var color = Color.Red;
			var rectangle = new Rectangle(leftTop, rightBottom, color);
			Assert.That(() => rectangle.Draw(canvas), Throws.Nothing);

			Received.InOrder(() => {
				canvas.Received(1).SetColor(Arg.Is(color));
				canvas.Received(1).DrawLine(Arg.Is(leftTop), Arg.Is(rightTop));
				canvas.Received(1).DrawLine(Arg.Is(rightTop), Arg.Is(rightBottom));
				canvas.Received(1).DrawLine(Arg.Is(rightBottom), Arg.Is(leftBottom));
				canvas.Received(1).DrawLine(Arg.Is(leftBottom), Arg.Is(leftTop));
			});
		}
	}
}