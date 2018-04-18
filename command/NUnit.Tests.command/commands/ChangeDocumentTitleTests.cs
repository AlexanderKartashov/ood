using NUnit.Framework;
using NSubstitute;
using command.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using command.document;
using System.Collections;
using NUnit.Framework.Constraints;

namespace command.commands.Tests
{
	[TestFixture]
	[Parallelizable]
	public class ChangeDocumentTitleTests
	{
		[TestCaseSource(typeof(Data), "TestCases")]
		public void ChangeDocumentTitleTest(IDocument document, string title, IResolveConstraint constraint)
		{
			Assert.That(() => new ChangeDocumentTitle(document, title), constraint);
		}

		[TestCase]
		public void TestExecute()
		{
			string newTitle = "newTitle";
			string oldTitle = "oldTitle";
			var document = new Document() { Title = oldTitle };
			var command = new ChangeDocumentTitle(document, newTitle);
			Assert.That(() => command.Execute(), Throws.Nothing);
			Assert.That(document.Title, Is.EqualTo(newTitle));
		}

		[TestCase]
		public void TestUnexecute()
		{
			string newTitle = "newTitle";
			string oldTitle = "oldTitle";
			var document = new Document() { Title = oldTitle };
			var command = new ChangeDocumentTitle(document, newTitle);
			Assert.That(() => command.Execute(), Throws.Nothing);
			Assert.That(document.Title, Is.EqualTo(newTitle));
			Assert.That(() => command.Unexecute(), Throws.Nothing);
			Assert.That(document.Title, Is.EqualTo(oldTitle));
		}

		[TestCase]
		public void TestDestroy()
		{
			string newTitle = "newTitle";
			string oldTitle = "oldTitle";
			var document = new Document() { Title = oldTitle };
			var command = new ChangeDocumentTitle(document, newTitle);
			Assert.That(() => command.Dispose(), Throws.Nothing);
		}

		class Data
		{
			public static IEnumerable TestCases
			{
				get {
					yield return new TestCaseData(null, null, Throws.ArgumentNullException);
					yield return new TestCaseData(null, "title", Throws.ArgumentNullException);
					yield return new TestCaseData(new Document(), null, Throws.ArgumentNullException);
					yield return new TestCaseData(new Document(), "title", Throws.Nothing);
				}
			}
		}
	}
}