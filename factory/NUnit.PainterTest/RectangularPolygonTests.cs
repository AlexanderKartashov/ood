using NSubstitute;
using NUnit.Framework;
using painter.shapes;
using painter.sdk;

namespace painter.Tests
{
	[TestFixture]
	[Parallelizable]
	public class RectangularPolygonTests
	{
		[Test]
		public void InitRectangularPolygonTest()
		{
			Assert.That(() => new RectangularPolygon(null, 0, 0, Color.Black), Throws.ArgumentNullException);
			Assert.That(() => new RectangularPolygon(new Point(0, 0), 0, 0, Color.Black), Throws.ArgumentException);
			Assert.That(() => new RectangularPolygon(new Point(0, 0), 1, 0, Color.Black), Throws.ArgumentException);
			Assert.That(() => new RectangularPolygon(new Point(0, 0), 1, 1, Color.Black), Throws.ArgumentException);
			Assert.That(() => new RectangularPolygon(new Point(0, 0), 1, 2, Color.Black), Throws.ArgumentException);
			Assert.That(() => new RectangularPolygon(new Point(0, 0), 1, 3, Color.Black), Throws.Nothing);
		}

		[Test]
		public void DrawRectangularPolygonTest()
		{
			var canvas = Substitute.For<ICanvas>();
			var center = new Point(0, 0);
			var color = Color.Red;
			var poly = new RectangularPolygon(center, 10, 5, color);
			Assert.That(() => poly.Draw(canvas), Throws.Nothing);

			canvas.Received(1).SetColor(Arg.Is(color));
			canvas.Received(5).DrawLine(Arg.Any<Point>(), Arg.Any<Point>());
		}
	}
}