using NUnit.Framework;
using command.commandFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using command.document;
using System.Collections;
using NUnit.Framework.Constraints;
using command.commands;

namespace command.commandFactory.Tests
{
	[TestFixture]
	[Parallelizable]
	public class InputParserTests
	{
		[TestCaseSource(typeof(Data), "TestCases")]
		public void InputParserTest(IDocument document, IDocumentItemFactory documentItemFactory, IResolveConstraint constraint)
		{
			Assert.That(() => new InputParser(document, documentItemFactory), constraint);
		}

		[Test]
		public void PropertiesTest()
		{
			var document = Substitute.For<IDocument>();
			var documentFactory = Substitute.For<IDocumentItemFactory>();
			var parser = new InputParser(document, documentFactory);
			Assert.That(parser.HelpText, Is.Not.Null);
		}

		[TestCase("")]
		[TestCase("fdf")]
		[TestCase("helpp")]
		[TestCase("104")]
		public void ParseInputFailedTest(string data)
		{
			var document = Substitute.For<IDocument>();
			var documentFactory = Substitute.For<IDocumentItemFactory>();
			var parser = new InputParser(document, documentFactory);
			Assert.That(parser.ParseInput(data), Is.Null);
		}

		[TestCase("Resizeimage ")]
		[TestCase("insertImage 10 11 sasd 100")]
		[TestCase("deleteItem aa")]
		public void ParseInputFailedThrowTest(string data)
		{
			var document = Substitute.For<IDocument>();
			var documentFactory = Substitute.For<IDocumentItemFactory>();
			var parser = new InputParser(document, documentFactory);
			Assert.That(() => parser.ParseInput(data), Throws.ArgumentException);
		}

		[TestCase("ReSiZeImAge 2 1 20")]
		public void ParserInputFailedIndexTest(string data)
		{
			var document = Substitute.For<IDocument>();
			var documentFactory = Substitute.For<IDocumentItemFactory>();
			var parser = new InputParser(document, documentFactory);
			Assert.That(() => parser.ParseInput(data), Throws.TypeOf<CommandError>());
		}

		[TestCase("Help")]
		[TestCase("List")]
		[TestCase("undo")]
		[TestCase("redO")]
		[TestCase("DeleteItem 0")]
		[TestCase("Save path to file")]
		[TestCase("ReplaceText 0 newText")]
		[TestCase("InsertParagraph 0 paragraph")]
		[TestCase("insertImagE 0 10 20 path")]
		public void ParserInputSucceededTest(string data)
		{
			var document = Substitute.For<IDocument>();
			var documentFactory = Substitute.For<IDocumentItemFactory>();
			var parser = new InputParser(document, documentFactory);
			Assert.That(parser.ParseInput(data), Is.Not.Null);
		}

		class Data
		{
			public static IEnumerable TestCases
			{
				get
				{
					yield return new TestCaseData(null, null, Throws.ArgumentNullException);
					yield return new TestCaseData(Substitute.For<IDocument>(), null, Throws.ArgumentNullException);
					yield return new TestCaseData(null, Substitute.For<IDocumentItemFactory>(), Throws.ArgumentNullException);
					yield return new TestCaseData(Substitute.For<IDocument>(), Substitute.For<IDocumentItemFactory>(), Throws.Nothing);
				}
			}
		}
	}
}