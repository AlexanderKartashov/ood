using NUnit.Framework;
using command.document.visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using System.IO;
using command.externals;
using command.storage;

namespace command.document.visitors.Tests
{
	[TestFixture]
	[Parallelizable]
	public class SaveVisitorTests
	{
		[Test]
		public void TestVisit()
		{
			var encoder = Substitute.For<IHtmlEncoder>();
			var fileSystem = Substitute.For<IFileSystem>();
			var fileStorage = Substitute.For<IFileStorage>();
			var path = "directory";
			fileSystem.IsAbsPath(path).Returns(true);
			var listVisitor = new SaveVisitor(fileStorage, encoder, fileSystem, path);
			var document = Substitute.For<IDocument>();
			var tw = Substitute.For<TextWriter>();
			document.ItemsCount.Returns(2);
			document.GetItem(Arg.Is(0)).Returns(Substitute.For<IImage>());
			document.GetItem(Arg.Is(1)).Returns(Substitute.For<IParagraph>());
			Assert.That(() => listVisitor.VisitDocument(document, tw), Throws.Nothing);
			fileSystem.Received(1).IsAbsPath(Arg.Is(path));
			tw.Received(5).WriteLine(Arg.Any<string>());
		}
	}
}