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
	public class RectangularPolygonParserTests
	{
		[TestCase("ct{1, 2} r{3} vc{4} c{red}")]
		public void ParseRectangularPolygonSucceededTest(string testData)
		{
			var parser = new RectangularPolygonParser();
			Shape shape = null;
			Assert.That(() => shape = parser.Parse(testData), Throws.Nothing);
			Assert.That(shape, Is.TypeOf<RectangularPolygon>());
		}

		[TestCase("")]
		[TestCase("ct{}")]
		[TestCase("ct{2}")]
		[TestCase("ct{1, 2}")]
		[TestCase("ct{1, 2} r{}")]
		[TestCase("ct{1, 2} r{3, 4} vc{4} c{red}")]
		[TestCase("ct{1, 2} r{3} vc{4, 5} c{red}")]
		[TestCase("ct{s, 2} r{3} vc{4} c{red}")]
		[TestCase("ct{1, 2} r{s} vc{4} c{red}")]
		[TestCase("ct{1, 2} r{3} vc{r} c{red}")]
		[TestCase("ct{1, 2} r{3} vc{4} c{55}")]
		[TestCase("a{1, 2} b{3} d{4} e{red}")]
		[TestCase("ct{1, 2} r{3} vc{4} c{red} 123")]
		[TestCase("123 ct{1, 2} r{3} vc{4} c{red}")]
		public void ParseRectangularPolygonFailedTest(string testData)
		{
			var parser = new RectangularPolygonParser();
			Assert.That(() => parser.Parse(testData), Throws.ArgumentException);
		}
	}
}