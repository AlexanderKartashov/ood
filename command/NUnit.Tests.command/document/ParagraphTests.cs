using NUnit.Framework;
using command.document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;
using System.Collections;

namespace command.document.Tests
{
	[TestFixture]
	[Parallelizable]
	public class ParagraphTests
	{
		[TestCaseSource(typeof(Data), "TestCases")]
		public void  TestInit(string text, IResolveConstraint constraint)
		{
			Assert.That(() => new Paragraph(text), constraint);
		}

		[Test]
		public void TestTitleChange()
		{
			var paragraph = new Paragraph("old text");
			var newText = "new text";
			Assert.That(() => paragraph.Text = newText, Throws.Nothing);
			Assert.That(() => paragraph.Text, Is.EqualTo(newText));
		}

		class Data
		{
			public static IEnumerable TestCases
			{
				get
				{
					yield return new TestCaseData(null, Throws.ArgumentNullException);
					yield return new TestCaseData("text", Throws.Nothing);
				}
			}
		}
	}
}