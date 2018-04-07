using NUnit.Framework;
using painter.shapes;
using painter.parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using painter_declarations;

namespace painter.Tests
{
	[TestFixture]
	[Parallelizable]
	public class EllipseParserTests
	{
		[TestCase("ct{1,2} sz{3, 4}c{red}")]
		public void ParseEllipseSucceededTest(string testData)
		{
			var parser = new EllipseParser();
			Shape shape = null;
			Assert.That(parser.ShapeType, Is.EqualTo("ellipse"));
			Assert.That(() => shape = parser.Parse(testData), Throws.Nothing);
			Assert.That(shape, Is.TypeOf<Ellipse>());
		}

		[TestCase("")]
		[TestCase("ct")]
		[TestCase("ct{}")]
		[TestCase("ct{1}")]
		[TestCase("ct{1.2}")]
		[TestCase("ct{a,2}")]
		[TestCase("ct{a,2}")]
		[TestCase("ct{1,2}sz{1,2}")]
		[TestCase("cz{1,2}sz{1,2}c{undefined}")]
		[TestCase("ct{1,2} sz{3, 4},c{red}123")]
		[TestCase("123 lc{1,2} sz{3, 4},c{red}")]
		[TestCase("v1{1,2} v2{3, 4} r{red}")]
		public void ParseEllipseFailedTest(string testData)
		{
			var parser = new EllipseParser();
			Assert.That(() => parser.Parse(testData), Throws.ArgumentException);
		}
	}
}