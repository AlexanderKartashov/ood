using NSubstitute;
using NUnit.Framework;
using painter.shapes;
using painter.sdk;

namespace painter.Tests
{
	[TestFixture]
	[Parallelizable]
	public class TriangleTests
	{
		private static Point?[] Values = { null, new Point(0, 0) };

		[Test]
		public void InitTriangleTest(
			[ValueSource("Values")] Point? vertex1,
			[ValueSource("Values")] Point? vertex2,
			[ValueSource("Values")] Point? vertex3
			)
		{
			if (vertex1 != null && vertex2 != null && vertex3 != null)
			{
				Assert.That(() => new Triangle(vertex1, vertex2, vertex3, Color.Black), Throws.Nothing);
			}
			else
			{
				Assert.That(() => new Triangle(vertex1, vertex2, vertex3, Color.Black), Throws.ArgumentNullException);
			}
		}

		[Test]
		public void DrawTriangleTest()
		{
			var canvas = Substitute.For<ICanvas>();
			var vertex1 = new Point(10, 20);
			var vertex2 = new Point(60, 20);
			var vertex3 = new Point(60, 15);
			var color = Color.Red;
			var ellipse = new Triangle(vertex1, vertex2, vertex3, color);
			Assert.That(() => ellipse.Draw(canvas), Throws.Nothing);

			Received.InOrder(() => {
				canvas.Received(1).SetColor(Arg.Is(color));
				canvas.Received(1).DrawLine(Arg.Is(vertex1), Arg.Is(vertex2));
				canvas.Received(1).DrawLine(Arg.Is(vertex2), Arg.Is(vertex3));
				canvas.Received(1).DrawLine(Arg.Is(vertex3), Arg.Is(vertex1));
			});
		}
	}
}