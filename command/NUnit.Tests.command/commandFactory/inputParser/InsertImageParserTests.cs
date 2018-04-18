using NUnit.Framework;
using command.commandFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using command.document;
using command.commands;
using NUnit.Framework.Constraints;
using NSubstitute;

namespace command.commandFactory.Tests
{
	[TestFixture]
	[Parallelizable]
	public class InsertImageParserTests
	{
		[TestCaseSource(typeof(Data), "TestCases")]
		public void InsertImageParserTest(IDocument document, IDocumentItemFactory factory, IResolveConstraint constraint)
		{
			Assert.That(() => new InsertImageParser(document, factory), constraint);
		}

		[TestCase("")]
		[TestCase("  ")]
		[TestCase(" 10 ")]
		[TestCase(" 10 20 path ")]
		[TestCase(" 10  patj ")]
		[TestCase(" a b patj ")]
		[TestCase(" 10 20 patj ")]
		[TestCase(" 10 20 patj ")]
		public void ParseCommandFailedTest(string data)
		{
			var document = Substitute.For<IDocument>();
			var factory = Substitute.For<IDocumentItemFactory>();
			var parser = new InsertImageParser(document, factory);
			Assert.That(() => parser.ParseCommand(data), Throws.ArgumentException);
		}

		[TestCase("0 10 20 path")]
		[TestCase("end 10 20 path")]
		public void ParseCommandSucceeded(string data)
		{
			var document = Substitute.For<IDocument>();
			var factory = Substitute.For<IDocumentItemFactory>();
			factory.CreateImage(Arg.Any<string>(), Arg.Any<uint>(), Arg.Any<uint>()).Returns(Substitute.For<IImage>());
			var parser = new InsertImageParser(document, factory);
			CommandContainer command;
			Assert.That(() => { command = parser.ParseCommand(data); }, Throws.Nothing);
			Assert.That(command, Is.Not.Null);
			Assert.That(command.Command, Is.TypeOf<InsertItem>());
			factory.Received(1).CreateImage(Arg.Any<string>(), Arg.Any<uint>(), Arg.Any<uint>());
			factory.DidNotReceive().CreateParagraph(Arg.Any<string>());
		}

		[Test]
		public void ParserPropertiesTest()
		{
			var document = Substitute.For<IDocument>();
			var factory = Substitute.For<IDocumentItemFactory>();
			var parser = new InsertImageParser(document, factory);
			Assert.That(parser.HelpString, Is.Not.Null);
			Assert.That(parser.CommandName, Is.EqualTo("InsertImage"));
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