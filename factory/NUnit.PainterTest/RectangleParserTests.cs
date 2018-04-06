using NUnit.Framework;
using painter.parsers;
using painter.shapes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter.Tests
{
	[TestFixture]
	[Parallelizable]
	public class RectangleParserTests
	{
		[TestCase("lt{1,2} rb{3, 4} c{red}")]
		public void ParseRectangleSucceededTest(string testData)
		{
			var parser = new RectangleParser();
			Shape shape = null;
			Assert.That(parser.ShapeType, Is.EqualTo("rectangle"));
			Assert.That(() => shape = parser.Parse(testData), Throws.Nothing);
			Assert.That(shape, Is.TypeOf<Rectangle>());
		}

		[TestCase("")]
		[TestCase("lt")]
		[TestCase("lt{}")]
		[TestCase("lt{1}")]
		[TestCase("lt{1.2}")]
		[TestCase("lt{a,2}")]
		[TestCase("lt{a,2}")]
		[TestCase("lt{1,2}rb{1,2}")]
		[TestCase("sz{1,2}rb{1,2}c{undefined}")]
		[TestCase("lt{1,2} rb{3, 4},c{red}123")]
		[TestCase("123 lt{1,2} rb{3, 4},c{red}")]
		[TestCase("v1{1,2} v2{3, 4} r{red}")]
		public void ParseRectangleFailedTest(string testData)
		{
			var parser = new RectangleParser();
			Assert.That(() => parser.Parse(testData), Throws.ArgumentException);
		}
	}
}