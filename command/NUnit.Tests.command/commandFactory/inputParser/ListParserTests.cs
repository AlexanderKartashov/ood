using NUnit.Framework;
using NSubstitute;
using command.commandFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using command.document;
using System.Collections;
using NUnit.Framework.Constraints;

namespace command.commandFactory.Tests
{
	[TestFixture]
	[Parallelizable]
	public class ListParserTests
	{
		[TestCaseSource(typeof(Data), "TestCases")]
		public void ListParserTest(IDocument document, IResolveConstraint constraint)
		{
			Assert.That(() => new ListParser(document), constraint);
		}

		[TestCase("")]
		public void ParseCommandTest(string data)
		{
			var document = Substitute.For<IDocument>();
			var parser = new ListParser(document);
			ListAction action;
			Assert.That(() => { action = parser.ParseCommand(data); }, Throws.Nothing);
			Assert.That(action, Is.Not.Null);
			Assert.That(action.Document, Is.EqualTo(document));
		}

		[Test]
		public void PropertiesTest()
		{
			var document = Substitute.For<IDocument>();
			var parser = new ListParser(document);
			Assert.That(parser.CommandName, Is.EqualTo("List"));
			Assert.That(parser.HelpString, Is.Not.Null);
		}

		class Data
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