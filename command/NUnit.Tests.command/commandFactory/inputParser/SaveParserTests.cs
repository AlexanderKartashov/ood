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

namespace command.commandFactory.Tests
{
	[TestFixture]
	[Parallelizable]
	public class SaveParserTests
	{
		[TestCaseSource(typeof(Data), "TestCases")]
		public void SaveParserTest(IDocument document, IResolveConstraint constraint)
		{
			Assert.That(() => new SaveParser(document), constraint);
		}

		[TestCase("")]
		public void ParseCommandFailedTest(string data)
		{
			var document = Substitute.For<IDocument>();
			var parser = new SaveParser(document);
			Assert.That(() => parser.ParseCommand(data), Throws.ArgumentException);
		}

		[Test]
		public void ParserPropertiesTest()
		{
			var document = Substitute.For<IDocument>();
			var parser = new SaveParser(document);
			Assert.That(parser.HelpString, Is.Not.Null);
			Assert.That(parser.CommandName, Is.EqualTo("Save"));
		}

		[TestCase("path")]
		public void ParseCommandTest(string data)
		{
			var document = Substitute.For<IDocument>();
			var parser = new SaveParser(document);
			SaveAction action;
			Assert.That(() => { action = parser.ParseCommand(data); }, Throws.Nothing);
			Assert.That(action, Is.Not.Null);
			Assert.That(action.Document, Is.EqualTo(document));
			Assert.That(action.PathToSave, Is.EqualTo("path"));
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