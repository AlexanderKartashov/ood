using NUnit.Framework;
using composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;

namespace composite.Tests
{
	[TestFixture()]
	[Parallelizable]
	public class CompositeFrameTests
	{
		[Test]
		public void ReturnsEmptyRectcFromEmptyShapesList()
		{
			var compositeShapes = new CompositeFrame(new List<IShape>());
			Assert.That(compositeShapes.Frame, Is.EqualTo(new Rect(0, 0, 0, 0)));
		}

		[Test]
		public void NonIntersectingRectandles()
		{
			var shape1 = Substitute.For<IShape>();
			shape1.Frame.Returns(new Rect(0, 0, 10, 10));
			var shape2 = Substitute.For<IShape>();
			shape2.Frame.Returns(new Rect(20, 20, 35, 30));
			var compositeShapes = new CompositeFrame(new List<IShape>() { shape1, shape2 });
			Assert.That(compositeShapes.Frame, Is.EqualTo(new Rect(0, 0, 55, 50)));
		}

		[Test]
		public void IntersectingRetctangles()
		{
			var shape1 = Substitute.For<IShape>();
			shape1.Frame.Returns(new Rect(0, 0, 20, 30));
			var shape2 = Substitute.For<IShape>();
			shape2.Frame.Returns(new Rect(10, 10, 35, 30));
			var compositeShapes = new CompositeFrame(new List<IShape>() { shape1, shape2 });
			Assert.That(compositeShapes.Frame, Is.EqualTo(new Rect(0, 0, 45, 40)));
		}

		[Test]
		public void NestedRectangles()
		{
			var shape1 = Substitute.For<IShape>();
			shape1.Frame.Returns(new Rect(0, 0, 30, 30));
			var shape2 = Substitute.For<IShape>();
			shape2.Frame.Returns(new Rect(10, 10, 10, 10));
			var compositeShapes = new CompositeFrame(new List<IShape>() { shape1, shape2 });
			Assert.That(compositeShapes.Frame, Is.EqualTo(new Rect(0, 0, 30, 30)));
		}
		
		[Test]
		public void TestMoveShapes()
		{
			var shape1 = Substitute.For<IShape>();
			shape1.Frame.Returns(new Rect(0, 0, 10, 10));
			var shape2 = Substitute.For<IShape>();
			shape2.Frame.Returns(new Rect(20, 20, 35, 30));
			var compositeShapes = new CompositeFrame(new List<IShape>() { shape1, shape2 });
			Assert.That(compositeShapes.Frame, Is.EqualTo(new Rect(0, 0, 55, 50)));

			var newRect = new Rect(20, 20, 55, 50);
			Assert.That(() => compositeShapes.Frame = newRect, Throws.Nothing);
			shape1.Received(1).Frame = new Rect(20, 20, 10, 10);
			shape2.Received(1).Frame = new Rect(40, 40, 35, 30);
		}

		[Test]
		public void TestResize()
		{
			var shape1 = Substitute.For<IShape>();
			shape1.Frame.Returns(new Rect(0, 0, 10, 10));
			var shape2 = Substitute.For<IShape>();
			shape2.Frame.Returns(new Rect(20, 20, 40, 40));
			var compositeShapes = new CompositeFrame(new List<IShape>() { shape1, shape2 });
			Assert.That(compositeShapes.Frame, Is.EqualTo(new Rect(0, 0, 60, 60)));

			var newRect = new Rect(0, 0, 30, 30);
			Assert.That(() => compositeShapes.Frame = newRect, Throws.Nothing);
			shape1.Received(1).Frame = new Rect(0, 0, 5, 5);
			shape2.Received(1).Frame = new Rect(10, 10, 20, 20);
		}

		[Test]
		public void TestResize2()
		{
			var shape1 = Substitute.For<IShape>();
			shape1.Frame.Returns(new Rect(0, 0, 10, 10));
			var shape2 = Substitute.For<IShape>();
			shape2.Frame.Returns(new Rect(20, 20, 40, 40));
			var compositeShapes = new CompositeFrame(new List<IShape>() { shape1, shape2 });
			Assert.That(compositeShapes.Frame, Is.EqualTo(new Rect(0, 0, 60, 60)));

			var newRect = new Rect(0, 0, 90, 90);
			Assert.That(() => compositeShapes.Frame = newRect, Throws.Nothing);
			shape1.Received(1).Frame = new Rect(0, 0, 15, 15);
			shape2.Received(1).Frame = new Rect(30, 30, 60, 60);
		}

		[Test]
		public void TestResizeAndMove()
		{
			var shape1 = Substitute.For<IShape>();
			shape1.Frame.Returns(new Rect(0, 0, 10, 10));
			var shape2 = Substitute.For<IShape>();
			shape2.Frame.Returns(new Rect(20, 20, 40, 40));
			var compositeShapes = new CompositeFrame(new List<IShape>() { shape1, shape2 });
			Assert.That(compositeShapes.Frame, Is.EqualTo(new Rect(0, 0, 60, 60)));

			var newRect = new Rect(10, 10, 90, 90);
			Assert.That(() => compositeShapes.Frame = newRect, Throws.Nothing);
			shape1.Received(1).Frame = new Rect(10, 10, 15, 15);
			shape2.Received(1).Frame = new Rect(40, 40, 60, 60);
		}
	}
}