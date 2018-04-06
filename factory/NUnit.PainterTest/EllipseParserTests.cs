using NUnit.Framework;
using painter.shapes;
using painter.parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace painter.Tests
{
	[TestFixture]
	[Parallelizable]
	public class EllipseParserTests
	{
		[TestCase("lt{1,2} sz{3, 4}c{red}")]
		public void ParseEllipseSucceededTest(string testData)
		{
			var parser = new EllipseParser();
			Shape shape = null;
			Assert.That(parser.ShapeType, Is.EqualTo("ellipse"));
			Assert.That(() => shape = parser.Parse(testData), Throws.Nothing);
			Assert.That(shape, Is.TypeOf<Ellipse>());
		}

		[TestCase("")]
		[TestCase("lt")]
		[TestCase("lt{}")]
		[TestCase("lt{1}")]
		[TestCase("lt{1.2}")]
		[TestCase("lt{a,2}")]
		[TestCase("lt{a,2}")]
		[TestCase("lt{1,2}sz{1,2}")]
		[TestCase("sz{1,2}sz{1,2}c{undefined}")]
		[TestCase("lt{1,2} sz{3, 4},c{red}123")]
		[TestCase("123 lt{1,2} sz{3, 4},c{red}")]
		[TestCase("v1{1,2} v2{3, 4} r{red}")]
		public void ParseEllipseFailedTest(string testData)
		{
			var parser = new EllipseParser();
			Assert.That(() => parser.Parse(testData), Throws.ArgumentException);
		}
	}
}