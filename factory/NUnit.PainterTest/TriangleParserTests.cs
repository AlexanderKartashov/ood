using NUnit.Framework;
using painter.parsers;
using painter.shapes;
using painter.sdk;

namespace painter.Tests
{
	[TestFixture]
	[Parallelizable]
	public class TriangleParserTests
	{
		[TestCase("v1{1,2} v2{3, 4}\t\tv3{5, \t6}c{red}")]
		public void ParseTriangleSucceededTest(string testData)
		{
			var parser = new TriangleParser();
			Shape shape = null;
			Assert.That(parser.ShapeType, Is.EqualTo("triangle"));
			Assert.That(() => shape = parser.Parse(testData), Throws.Nothing);
			Assert.That(shape, Is.TypeOf<Triangle>());
		}

		[TestCase("")]
		[TestCase("v1")]
		[TestCase("v1{}")]
		[TestCase("v1{1}")]
		[TestCase("v1{1.2}")]
		[TestCase("v1{a,2}")]
		[TestCase("v1{a,2}")]
		[TestCase("v1{1,2}v2{1,2}v3{1,2}")]
		[TestCase("v1{1,2}v2{1,2}v3{1,2}c{undefined}")]
		[TestCase("v1{1,2} v2{3, 4}\t\tv3{5, \t6}c{red}123")]
		[TestCase("123 v1{1,2} v2{3, 4}\t\tv3{5, \t6}c{red}")]
		[TestCase("s1{1,2} s2{3, 4}\t\ts3{5, \t6}s{red}")]
		public void ParseTriangleFailedTest(string testData)
		{
			var parser = new TriangleParser();
			Assert.That(() => parser.Parse(testData), Throws.ArgumentException);
		}
	}
}