using NUnit.Framework;
using NSubstitute;
using command.document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using NUnit.Framework.Constraints;

namespace command.document.Tests
{
	[TestFixture]
	[Parallelizable]
	public class DocumentTitleMementoTests
	{
		[TestCaseSource(typeof(Data), "TestCases")]
		public void DocumentTitleMementoTest(IDocument document, string title, IResolveConstraint constraint)
		{
			Assert.That(() => new DocumentTitleMemento(document, title), constraint);
		}

		[Test]
		public void RestoreTest()
		{
			var document = Substitute.For<IDocument>();
			var oldTitle = "old title";
			var memento = new DocumentTitleMemento(document, oldTitle);
			document.Title = "new title";
			document.ClearReceivedCalls();
			Assert.That(() => memento.Restore(), Throws.Nothing);
			Assert.That(document.Title, Is.EqualTo(oldTitle));
			document.Received(1).Title = oldTitle;
		}

		class Data
		{
			public static IEnumerable TestCases
			{
				get
				{
					yield return new TestCaseData(null, null, Throws.ArgumentNullException);
					yield return new TestCaseData(null, "title", Throws.ArgumentNullException);
					yield return new TestCaseData(new Document(), null, Throws.ArgumentNullException);
					yield return new TestCaseData(new Document(), "title", Throws.Nothing);
				}
			}
		}
	}
}