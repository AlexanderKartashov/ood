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
	public class SetTitleParserTests
	{
		[TestCaseSource(typeof(Data), "TestCases")]
		public void SetTitleParserTest(IDocument document, IResolveConstraint constraint)
		{
			Assert.That(() => new SetTitleParser(document), constraint);
		}

		[TestCase("")]
		public void ParseCommandFailedTest(string data)
		{
			var document = Substitute.For<IDocument>();
			var parser = new SetTitleParser(document);
			Assert.That(() => parser.ParseCommand(data), Throws.ArgumentException);
		}

		[TestCase("new title")]
		public void ParseCommandSucceededTest(string data)
		{
			var document = Substitute.For<IDocument>();
			var parser = new SetTitleParser(document);
			CommandContainer container;
			Assert.That(() => { container = parser.ParseCommand(data); }, Throws.Nothing);
			Assert.That(container.Command, Is.Not.Null);
			Assert.That(container.Command, Is.TypeOf<FunctionalCommand>());
		}

		[Test]
		public void ParserPropertiesTest()
		{
			var document = Substitute.For<IDocument>();
			var parser = new SetTitleParser(document);
			Assert.That(parser.HelpString, Is.Not.Null);
			Assert.That(parser.CommandName, Is.EqualTo("SetTitle"));
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