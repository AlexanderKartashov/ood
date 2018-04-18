using NUnit.Framework;
using command.document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using NUnit.Framework.Constraints;
using NSubstitute;

namespace command.document.Tests
{
	[TestFixture]
	[Parallelizable]
	public class ParagraphMementoTests
	{
		[TestCaseSource(typeof(Data), "TestCases")]
		public void ParagraphMementoTest(IParagraph paragraph, string text, IResolveConstraint constraint)
		{
			Assert.That(() => new ParagraphMemento(paragraph, text), constraint);
		}

		[Test(Description = "paragraph memento")]
		public void RestoreTest()
		{
			var paragraph = Substitute.For<IParagraph>();
			var oldText = "old text";
			var memento = new ParagraphMemento(paragraph, oldText);
			paragraph.Text = "new text";
			paragraph.ClearReceivedCalls();
			Assert.That(() => memento.Restore(), Throws.Nothing);
			Assert.That(paragraph.Text, Is.EqualTo(oldText));
			paragraph.Received(1).Text = oldText;
		}

		class Data
		{
			public static IEnumerable TestCases
			{
				get
				{
					yield return new TestCaseData(null, null, Throws.ArgumentNullException);
					yield return new TestCaseData(null, "text", Throws.ArgumentNullException);
					yield return new TestCaseData(new Paragraph("text"), null, Throws.ArgumentNullException);
					yield return new TestCaseData(new Paragraph("text"), "text", Throws.Nothing);
				}
			}
		}
	}
}