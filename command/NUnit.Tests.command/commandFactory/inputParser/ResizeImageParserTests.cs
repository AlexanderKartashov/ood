using NUnit.Framework;
using command.commandFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using command.document;
using NUnit.Framework.Constraints;
using NSubstitute;
using command.commands;

namespace command.commandFactory.Tests
{
	[TestFixture]
	[Parallelizable]
	public class ResizeImageParserTests
	{
		[TestCaseSource(typeof(Data), "TestCases")]
		public void ResizeImageParserTest(IDocument document, IResolveConstraint constraint)
		{
			Assert.That(() => new ResizeImageParser(document), constraint);
		}

		[TestCase("")]
		[TestCase(" 0 10 10")]
		[TestCase("0")]
		[TestCase("0 0")]
		[TestCase("0 0 a")]
		[TestCase("a b c")]
		[TestCase("1 2 3 ")]
		[TestCase(" 1 2 3")]
		[TestCase(" 1 2 3 ")]
		public void ParseCommandSucceededTest(string data)
		{
			var document = Substitute.For<IDocument>();
			var parser = new ResizeImageParser(document);
			Assert.That(() => parser.ParseCommand(data), Throws.ArgumentException);
		}

		[TestCase("1 2 3")]
		public void ParseCommandFailedTest(string data)
		{
			var document = Substitute.For<IDocument>();
			var parser = new ResizeImageParser(document);
			CommandContainer command;
			Assert.That(() => { command = parser.ParseCommand(data); }, Throws.Nothing);
			Assert.That(command, Is.Not.Null);
			Assert.That(command.Command, Is.TypeOf<ResizeImage>());
		}

		[Test]
		public void ParserPropertiesTest()
		{
			var document = Substitute.For<IDocument>();
			var parser = new ResizeImageParser(document);
			Assert.That(parser.HelpString, Is.Not.Null);
			Assert.That(parser.CommandName, Is.EqualTo("ResizeImage"));
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