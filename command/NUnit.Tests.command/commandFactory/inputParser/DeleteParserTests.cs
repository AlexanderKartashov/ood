using NUnit.Framework;
using command.commandFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using command.commands;
using NSubstitute;
using command.document;
using NUnit.Framework.Constraints;

namespace command.commandFactory.Tests
{
	[TestFixture]
	[Parallelizable]
	public class DeleteParserTests
	{
		[TestCaseSource(typeof(Data), "TestCases")]
		public void DeleteParserTest(IDocument document, IResolveConstraint constraint)
		{
			Assert.That(() => new DeleteParser(document), constraint);
		}

		[TestCase("")]
		[TestCase("a")]
		[TestCase("1a")]
		[TestCase(" 3 ")]
		[TestCase(" 2")]
		[TestCase("1 ")]
		public void ParseCommandFailedTest(string data)
		{
			var document = Substitute.For<IDocument>();
			var parser = new DeleteParser(document);
			Assert.That(() => parser.ParseCommand(data), Throws.ArgumentException);
		}

		[TestCase("0")]
		public void ParseCommandSucceedeTest(string data)
		{
			var document = Substitute.For<IDocument>();
			var parser = new DeleteParser(document);
			CommandContainer command;
			Assert.That(() => { command = parser.ParseCommand(data); }, Throws.Nothing);
			Assert.That(command, Is.Not.Null);
			Assert.That(command.Command, Is.TypeOf<DeleteItem>());
		}

		[Test]
		public void PropertiesTest()
		{
			var document = Substitute.For<IDocument>();
			var parser = new DeleteParser(document);
			Assert.That(parser.HelpString, Is.Not.Null);
			Assert.That(parser.CommandName, Is.EqualTo("DeleteItem"));
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