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
	public class ReplaceTextParserTests
	{
		[TestCaseSource(typeof(Data), "TestCases")]
		public void ReplaceTextParserTest(IDocument document, IResolveConstraint constraint)
		{
			Assert.That(() => new ReplaceTextParser(document), constraint);
		}

		[TestCase("")]
		[TestCase("0")]
		[TestCase(" 0 text")]
		[TestCase("a text")]
		public void ParseCommandFailedTest(string data)
		{
			var document = Substitute.For<IDocument>();
			var parser = new ReplaceTextParser(document);
			Assert.That(() => parser.ParseCommand(data), Throws.ArgumentException);
		}

		[TestCase("0 text")]
		public void ParseCommandSucceededTest(string data)
		{
			var document = Substitute.For<IDocument>();
			var parser = new ReplaceTextParser(document);
			CommandContainer command;
			Assert.That(() => { command = parser.ParseCommand(data); }, Throws.Nothing);
			Assert.That(command, Is.Not.Null);
			Assert.That(command.Command, Is.TypeOf<FunctionalCommand>());
		}

		[Test]
		public void PropertiesParserTest()
		{
			var document = Substitute.For<IDocument>();
			var parser = new ReplaceTextParser(document);
			Assert.That(parser.CommandName, Is.EqualTo("ReplaceText"));
			Assert.That(parser.HelpString, Is.Not.Null);
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