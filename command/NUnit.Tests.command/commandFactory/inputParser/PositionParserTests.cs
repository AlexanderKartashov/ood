using NUnit.Framework;
using command.commandFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using command.document;
using NSubstitute;
using System.Collections;
using NUnit.Framework.Constraints;

namespace command.commandFactory.Tests
{
	[TestFixture]
	[Parallelizable]
	public class PositionParserTests
	{
		[TestCaseSource(typeof(Data), "TestCases")]
		public void PositionParserTest(IDocument document, IResolveConstraint constraint)
		{
			Assert.That(() => new PositionParser(document), constraint);
		}

		[TestCase("")]
		[TestCase("a")]
		[TestCase("end ")]
		[TestCase(" end")]
		[TestCase(" end ")]
		[TestCase("0 0")]
		public void ParsePositionFailedTest(string data)
		{
			var document = Substitute.For<IDocument>();
			var parser = new PositionParser(document);
			Assert.That(() => parser.ParsePosition(data), Throws.TypeOf<FormatException>());
		}

		[TestCase("2", 2)]
		[TestCase("end", 1)]
		public void ParsePositionTest(string data, int expected)
		{
			var document = Substitute.For<IDocument>();
			document.ItemsCount.Returns(1);

			var parser = new PositionParser(document);
			int value = -1;
			Assert.That(() => { value = parser.ParsePosition(data); }, Throws.Nothing);
			Assert.That(value, Is.EqualTo(expected));
		}

		struct Data
		{
			public static IEnumerable TestCases
			{
				get
				{
					yield return new TestCaseData(null, Throws.ArgumentNullException);
					yield return new TestCaseData(Substitute.For<IDocument>(), Throws.Nothing);
				}
			}
		}
	}
}