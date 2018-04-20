using NUnit.Framework;
using NSubstitute;
using command.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using command.document;
using NUnit.Framework.Constraints;

namespace command.commands.Tests
{
	[TestFixture]
	[Parallelizable]
	public class ChangeParagraphTextTests
	{
		[TestCaseSource(typeof(Data), "TestCases")]
		public void ChangeParagraphTextTest(IParagraph paragraph, string text, IResolveConstraint constraint)
		{
			Assert.That(() => new ChangeParagraphText(paragraph, text), constraint);
		}

		[TestCase]
		public void TestExecute()
		{
			string newText = "newText";
			string oldText = "oldText";
			var paragraph = new Paragraph(oldText);
			var command = new ChangeParagraphText(paragraph, newText);
			Assert.That(() => command.Execute(), Throws.Nothing);
			Assert.That(paragraph.Text, Is.EqualTo(newText));
		}

		[TestCase]
		public void TestUnexecute()
		{
			string newText = "newText";
			string oldText = "oldText";
			var paragraph = new Paragraph(oldText);
			var command = new ChangeParagraphText(paragraph, newText);
			Assert.That(() => command.Execute(), Throws.Nothing);
			Assert.That(paragraph.Text, Is.EqualTo(newText));
			Assert.That(() => command.Unexecute(), Throws.Nothing);
			Assert.That(paragraph.Text, Is.EqualTo(oldText));
		}

		[TestCase]
		public void TestDestroy()
		{
			string oldText = "oldText";
			string newText = "newText";
			var paragraph = new Paragraph(oldText);
			var command = new ChangeParagraphText(paragraph, newText);
			Assert.That(() => command.Execute(), Throws.Nothing);
			Assert.That(() => command.Dispose(), Throws.Nothing);
		}

		class Data
		{
			public static IEnumerable TestCases
			{
				get
				{
					yield return new TestCaseData(null, null, Throws.TypeOf<CommandError>());
					yield return new TestCaseData(null, "text", Throws.TypeOf<CommandError>());
					yield return new TestCaseData(new Paragraph("text"), null, Throws.ArgumentNullException);
					yield return new TestCaseData(new Paragraph("text"), "text", Throws.Nothing);
				}
			}
		}
	}
}