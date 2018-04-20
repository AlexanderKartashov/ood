using command.document;
using command.document.visitors;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit.Tests.command.document.visitors
{
	[TestFixture()]
	[Parallelizable]
	public class ListVisitorTests
	{
		[Test]
		public void TestVisit()
		{
			var listVisitor = new ListVisitor();
			var document = Substitute.For<IDocument>();
			var tw = Substitute.For<TextWriter>();
			document.ItemsCount.Returns(2);
			document.GetItem(Arg.Is(0)).Returns(Substitute.For<IImage>());
			document.GetItem(Arg.Is(1)).Returns(Substitute.For<IParagraph>());
			Assert.That(() => listVisitor.VisitDocument(document, tw), Throws.Nothing);
			tw.Received(3).WriteLine(Arg.Any<string>());
		}
	}
}
