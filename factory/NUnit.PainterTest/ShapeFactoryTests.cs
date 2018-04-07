using NUnit.Framework;
using NSubstitute;
using painter;
using painter.parsers;
using painter.shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using painter_declarations;

namespace painter.Tests
{
	[TestFixture]
	[Parallelizable]
	public class ShapeFactoryTests
	{
		[TestCase("type value")]
		public void CreateShapeTest(string testData)
		{
			var parser = Substitute.For<IShapeParser>();
			parser.ShapeType.Returns("type");
			parser.Parse(Arg.Is("value")).Returns(new Triangle(new Point(0, 0), new Point(0, 0), new Point(0, 0), Color.Red));
			var list = new List<IShapeParser>();
			list.Add(parser);
			var shapeFactory = new ShapeFactory(list);
			Assert.That(() => shapeFactory.CreateShape(testData), Throws.Nothing);
			parser.Received(1).Parse(Arg.Is("value"));
		}

		[TestCase("type")]
		[TestCase("type ")]
		[TestCase("undefined 11")]
		public void CreateShapeFailedTest(string testData)
		{
			var parser = Substitute.For<IShapeParser>();
			parser.ShapeType.Returns("type");
			var list = new List<IShapeParser>() { parser };
			var shapeFactory = new ShapeFactory(list);
			Assert.That(() => shapeFactory.CreateShape(testData), Throws.ArgumentException);
		}

		[TestCase("")]
		[TestCase(null)]
		public void CreateShapeFailedOnEmptyStringTest(string testData)
		{
			var parser = Substitute.For<IShapeParser>();
			parser.ShapeType.Returns("type");
			var list = new List<IShapeParser>() { parser };
			var shapeFactory = new ShapeFactory(list);
			Assert.That(() => shapeFactory.CreateShape(testData), Throws.ArgumentNullException);
		}

	}
}