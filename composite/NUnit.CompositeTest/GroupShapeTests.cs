using NUnit.Framework;
using System.Collections.Generic;
using NSubstitute;

namespace composite.Tests
{
	[TestFixture()]
	[Parallelizable]
	public class GroupShapeTests
	{
		[Test()]
		public void InsertTest()
		{
			var shape = Substitute.For<IShape>();
			var group = new GroupShape();
			Assert.That(group.Shapes, Is.Empty);
			Assert.That(() => group.Insert(shape), Throws.Nothing);
			Assert.That(group.Shapes, Is.EquivalentTo(new List<IShape>() { shape }));
		}

		[Test()]
		public void RemoveTest()
		{
			var shape = Substitute.For<IShape>();
			var group = new GroupShape();
			Assert.That(group.Shapes, Is.Empty);
			Assert.That(() => group.Insert(shape), Throws.Nothing);
			Assert.That(group.Shapes, Is.EquivalentTo(new List<IShape>() { shape }));

			var anotherShape = Substitute.For<IShape>();
			Assert.That(() => group.Remove(anotherShape), Throws.Nothing);
			Assert.That(group.Shapes, Is.EquivalentTo(new List<IShape>() { shape }));
			Assert.That(() => group.Remove(shape), Throws.Nothing);
			Assert.That(group.Shapes, Is.Empty);
		}

		[Test()]
		public void DrawTest()
		{
			var shape1 = Substitute.For<IShape>();
			var shape2 = Substitute.For<IShape>();
			var canvas = Substitute.For<ICanvas>();
			var group = new GroupShape();
			Assert.That(() => group.Insert(shape1), Throws.Nothing);
			Assert.That(() => group.Insert(shape2), Throws.Nothing);
			Assert.That(() => group.Draw(canvas), Throws.Nothing);

			Received.InOrder(() => {
				shape1.Draw(Arg.Is(canvas));
				shape2.Draw(Arg.Is(canvas));
			});
		}
	}
}