using NUnit.Framework;
using command.commandFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using command.document;
using NSubstitute;
using NUnit.Framework.Constraints;
using command.commands;

namespace command.commandFactory.Tests
{
	[TestFixture]
	[Parallelizable]
	public class InsertParagraphParserTests
	{
		[TestCaseSource(typeof(Data), "TestCases")]
		public void InsertParagraphParserTest(IDocument document, IDocumentItemFactory factory, IResolveConstraint constraint)
		{
			Assert.That(() => new InsertParagraphParser(document, factory), constraint);
		}

		[TestCase("")]
		[TestCase("  ")]
		[TestCase("0")]
		[TestCase(" 0")]
		[TestCase(" 0 ")]
		[TestCase(" end ")]
		[TestCase("end")]
		public void ParseCommandFailedTest(string data)
		{
			var document = Substitute.For<IDocument>();
			var factory = Substitute.For<IDocumentItemFactory>();
			var parser = new InsertParagraphParser(document, factory);
			Assert.That(() => parser.ParseCommand(data), Throws.ArgumentException);
		}

		[TestCase("0 path")]
		[TestCase("end 20 path")]
		public void ParseCommandSucceeded(string data)
		{
			var document = Substitute.For<IDocument>();
			var factory = Substitute.For<IDocumentItemFactory>();
			factory.CreateParagraph(Arg.Any<string>()).Returns(Substitute.For<IParagraph>());
			var parser = new InsertParagraphParser(document, factory);
			CommandContainer command;
			Assert.That(() => { command = parser.ParseCommand(data); }, Throws.Nothing);
			Assert.That(command, Is.Not.Null);
			Assert.That(command.Command, Is.TypeOf<InsertItem>());
			factory.DidNotReceive().CreateImage(Arg.Any<string>(), Arg.Any<uint>(), Arg.Any<uint>());
			factory.Received(1).CreateParagraph(Arg.Any<string>());
		}

		[Test]
		public void ParserPropertiesTest()
		{
			var document = Substitute.For<IDocument>();
			var factory = Substitute.For<IDocumentItemFactory>();
			var parser = new InsertParagraphParser(document, factory);
			Assert.That(parser.HelpString, Is.Not.Null);
			Assert.That(parser.CommandName, Is.EqualTo("InsertParagraph"));
		}


		struct Data
		{
			public static IEnumerable TestCases
			{
				get
				{
					yield return new TestCaseData(null, null, Throws.ArgumentNullException);
					yield return new TestCaseData(null, Substitute.For<IDocumentItemFactory>(), Throws.ArgumentNullException);
					yield return new TestCaseData(Substitute.For<IDocument>(), null, Throws.ArgumentNullException);
					yield return new TestCaseData(Substitute.For<IDocument>(), Substitute.For<IDocumentItemFactory>(), Throws.Nothing);
				}
			}
		}
	}
}