using command.document;
using command.document.visitors;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit.Tests.command.document
{
	[TestFixture]
	[Parallelizable]
	public class BuilderDirectorTests
	{
		[Test]
		public void TestVisitor()
		{
			var visitor = new BuilderDirector();
			var builder = Substitute.For<IDocumentContentBuilder>();
			var document = Substitute.For<IDocument>();
			document.GetItem(Arg.Is(0)).Returns(Substitute.For<IImage>());
			document.GetItem(Arg.Is(1)).Returns(Substitute.For<IParagraph>());
			document.ItemsCount.Returns(2);
			visitor.Build(builder, document);

			Received.InOrder(() =>
			{
				builder.BeforeBuild();
				builder.BuildTitle(Arg.Any<string>());
				builder.BuildImage(Arg.Any<IImage>());
				builder.BuildParagraph(Arg.Any<IParagraph>());
				builder.AfterBuild();
			});
		}
	}
}
