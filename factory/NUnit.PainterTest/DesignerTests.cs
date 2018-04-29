using NUnit.Framework;
using NSubstitute;
using System;
using System.Linq;
using System.IO;
using painter.shapes;
using painter.sdk;

namespace painter.Tests
{
	[TestFixture]
	[Parallelizable]
	public class DesignerTests
	{
		public void DesignerTest()
		{
			Assert.That(() => new Designer(null), Throws.ArgumentNullException);
			var shapeFactory = Substitute.For<IShapeFactory>();
			Assert.That(() => new Designer(shapeFactory), Throws.Nothing);
		}

		[Test]
		public void CreateDraftWithEmptyTextReaderTest()
		{
			var shapeFactory = Substitute.For<IShapeFactory>();
			var designer = new Designer(shapeFactory);
			PictureDraft draft = null;
			Assert.That(() => draft = designer.CreateDraft(null, null), Throws.Nothing);
			Assert.That(draft.Shapes.Count(), Is.Zero);
		}

		[Test]
		public void CreateDraftTest()
		{
			var shapeFactory = Substitute.For<IShapeFactory>();
			shapeFactory.CreateShape(Arg.Any<string>()).Returns(new Rectangle(new Point(0, 0), new Point(0, 0), Color.Red));

			var reader = Substitute.For<TextReader>();
			reader.ReadLine().Returns("shape description", "shape description", "");
			
			var designer = new Designer(shapeFactory);
			PictureDraft draft = null;
			Assert.That(() => draft = designer.CreateDraft(reader, null), Throws.Nothing);
			Assert.That(draft.Shapes.Count(), Is.EqualTo(2));

			shapeFactory.Received(2).CreateShape(Arg.Any<string>());
			reader.Received(3).ReadLine();
			Assert.That(draft.Shapes.Count(), Is.EqualTo(2));
		}

		[Test]
		public void CreateDraftWithErrorReporterTest()
		{
			var shapeFactory = Substitute.For<IShapeFactory>();
			shapeFactory.CreateShape(Arg.Any<string>()).Returns(
				x => new Rectangle(new Point(0, 0), new Point(0, 0), Color.Red),
				x => new Rectangle(new Point(0, 0), new Point(0, 0), Color.Red),
				x => { throw new ArgumentException("invalid text format"); },
				x => new Rectangle(new Point(0, 0), new Point(0, 0), Color.Red)
			);

			var reader = Substitute.For<TextReader>();
			reader.ReadLine().Returns("shape description", "shape description", "invalid shape description", "shape description", null);

			var writer = Substitute.For<TextWriter>();
			
			var designer = new Designer(shapeFactory);
			PictureDraft draft = null;
			Assert.That(() => draft = designer.CreateDraft(reader, writer), Throws.Nothing);
			Assert.That(draft.Shapes.Count(), Is.EqualTo(3));

			writer.Received(1).WriteLine(Arg.Any<string>());
			shapeFactory.Received(4).CreateShape(Arg.Any<string>());
			reader.Received(5).ReadLine();
			Assert.That(draft.Shapes.Count(), Is.EqualTo(3));
		}
	}
}