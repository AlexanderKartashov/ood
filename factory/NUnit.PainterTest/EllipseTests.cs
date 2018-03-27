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
	public class EllipseTests
	{
		private static Point?[] Values = { null, new Point(0, 0) };

		[Test]
		public void InitTest(
			[ValueSource("Values")] Point? leftTop,
			[ValueSource("Values")] Point? size)
		{
			if (leftTop != null && size != null)
			{
				Assert.That(() => new Ellipse(leftTop, size, Color.Black), Throws.Nothing);
			}
			else
			{
				Assert.That(() => new Ellipse(leftTop, size, Color.Black), Throws.ArgumentNullException);
			}
		}

		[Test]
		public void DrawTest()
		{
			var canvas = Substitute.For<ICanvas>();
			var leftTop = new Point(10, 15);
			var size = new Point(20, 25);
			var color = Color.Red;
			var ellipse = new Ellipse(leftTop, size, color);
			Assert.That(() => ellipse.Draw(canvas), Throws.Nothing);

			Received.InOrder(() => {
				canvas.Received(1).SetColor(Arg.Is(color));
				canvas.Received(1).DrawEllipse(Arg.Is(leftTop), Arg.Is(size));
			});
		}
	}
}