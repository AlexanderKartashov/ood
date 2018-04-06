using NSubstitute;
using NUnit.Framework;
using painter;
using painter.shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			Assert.Fail("not implemented");
			//var canvas = Substitute.For<ICanvas>();
			//var center = new Point(0, 0);
			//var color = Color.Red;
			//var ellipse = new RectangularPolygon(center, 10, 5, color);
			//Assert.That(() => ellipse.Draw(canvas), Throws.Nothing);
			//
			//Received.InOrder(() => {
			//	canvas.Received(1).SetColor(Arg.Is(color));
			//	//canvas.Received(1).DrawLine(Arg.Is(vertex1), Arg.Is(vertex2));
			//	//canvas.Received(1).DrawLine(Arg.Is(vertex2), Arg.Is(vertex3));
			//	//canvas.Received(1).DrawLine(Arg.Is(vertex3), Arg.Is(vertex1));
			//});
		}
	}
}