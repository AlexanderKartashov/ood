using NUnit.Framework;
using painter.parsers;
using painter_declarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter.Tests
{
	[TestFixture]
	[Parallelizable]
	public class ColorParserTests
	{
		[TestCase("Red", ExpectedResult = Color.Red)]
		[TestCase("Green", ExpectedResult = Color.Green)]
		[TestCase("Blue", ExpectedResult = Color.Blue)]
		[TestCase("Yellow", ExpectedResult = Color.Yellow)]
		[TestCase("Pink", ExpectedResult = Color.Pink)]
		[TestCase("Black", ExpectedResult = Color.Black)]
		[TestCase("red", ExpectedResult = Color.Red)]
		[TestCase("green", ExpectedResult = Color.Green)]
		[TestCase("blue", ExpectedResult = Color.Blue)]
		[TestCase("yellow", ExpectedResult = Color.Yellow)]
		[TestCase("pink", ExpectedResult = Color.Pink)]
		[TestCase("black", ExpectedResult = Color.Black)]
		public Color ParseColorSucceededTest(String value)
		{
			return ColorParser.Parse(value);
		}

		[TestCase("")]
		[TestCase("invalidColor")]
		[TestCase("55")]
		public void ParseColorFailedTest(String value)
		{
			Assert.That(() => ColorParser.Parse(value), Throws.ArgumentException);
		}
	}
}