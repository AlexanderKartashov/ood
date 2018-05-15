using NUnit.Framework;
using composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;

namespace composite.Tests
{
	[TestFixture()]
	[Parallelizable]
	public class CompositeLineStyleTests
	{
		[Test]
		public void TestReturnNullForEmptyCollection()
		{
			IEnumerable<ILineStyle> ls = new List<ILineStyle>();
			var style = new CompositeLineStyle(ls);
			Assert.That(style.Color, Is.Null);
			Assert.That(style.Enable, Is.Null);
			Assert.That(style.LineWidth, Is.Null);
		}

		[Test]
		public void TestReturnNullForCollectionWithNullValues()
		{
			var ls1 = Substitute.For<ILineStyle>();
			ls1.Color.Returns((RGBAColor?)null);
			ls1.Enable.Returns((bool?)null);
			ls1.LineWidth.Returns((uint?)null);
			var ls2 = Substitute.For<ILineStyle>();
			ls2.Color.Returns((RGBAColor?)null);
			ls2.Enable.Returns((bool?)null);
			ls2.LineWidth.Returns((uint?)null);

			IEnumerable<ILineStyle> ls = new List<ILineStyle>() { ls1, ls2 };
			var style = new CompositeLineStyle(ls);
			Assert.That(style.Color, Is.Null);
			Assert.That(style.Enable, Is.Null);
			Assert.That(style.LineWidth, Is.Null);
		}

		[Test()]
		public void TestReturnNullForCollectionNullValue()
		{
			var ls1 = Substitute.For<ILineStyle>();
			ls1.Color.Returns((RGBAColor?)null);
			ls1.Enable.Returns((bool?)null);
			ls1.LineWidth.Returns((uint?)null);
			var ls2 = Substitute.For<ILineStyle>();
			ls2.Color.Returns(new RGBAColor(0, 0, 0, 0));
			ls2.Enable.Returns(false);
			ls2.LineWidth.Returns(1u);

			IEnumerable<ILineStyle> ls = new List<ILineStyle>() { ls1, ls2 };
			var style = new CompositeLineStyle(ls);
			Assert.That(style.Color, Is.Null);
			Assert.That(style.Enable, Is.Null);
			Assert.That(style.LineWidth, Is.Null);
		}

		[Test()]
		public void TestReturnNullForCollectionNotequalNotNulllValues()
		{
			var ls1 = Substitute.For<ILineStyle>();
			ls1.Color.Returns(new RGBAColor(1, 2, 3, 4));
			ls1.Enable.Returns(true);
			ls1.LineWidth.Returns(0u);
			var ls2 = Substitute.For<ILineStyle>();
			ls2.Color.Returns(new RGBAColor(0, 0, 0, 0));
			ls2.Enable.Returns(false);
			ls2.LineWidth.Returns(1u);

			IEnumerable<ILineStyle> ls = new List<ILineStyle>() { ls1, ls2 };
			var style = new CompositeLineStyle(ls);
			Assert.That(style.Color, Is.Null);
			Assert.That(style.Enable, Is.Null);
			Assert.That(style.LineWidth, Is.Null);
		}

		[Test()]
		public void TestReturnNotNullForCollectionEqualNotNulllValues()
		{
			var expectedColor = new RGBAColor(0, 1, 2, 3);
			var expectedEnable = false;
			var expectedWidth = 2u;
			var ls1 = Substitute.For<ILineStyle>();
			ls1.Color.Returns(expectedColor);
			ls1.Enable.Returns(expectedEnable);
			ls1.LineWidth.Returns(expectedWidth);
			var ls2 = Substitute.For<ILineStyle>();
			ls2.Color.Returns(expectedColor);
			ls2.Enable.Returns(expectedEnable);
			ls2.LineWidth.Returns(expectedWidth);

			IEnumerable<ILineStyle> ls = new List<ILineStyle>() { ls1, ls2 };
			var style = new CompositeLineStyle(ls);
			Assert.That(style.Color, Is.EqualTo(expectedColor));
			Assert.That(style.Enable, Is.EqualTo(expectedEnable));
			Assert.That(style.LineWidth, Is.EqualTo(expectedWidth));
		}

		[Test]
		public void TestSetters()
		{
			var expectedColor = new RGBAColor(0, 1, 2, 3);
			var expectedEnable = false;
			var expectedWidth = 9u;

			var ls1 = Substitute.For<ILineStyle>();
			var ls2 = Substitute.For<ILineStyle>();

			IEnumerable<ILineStyle> ls = new List<ILineStyle>() { ls1, ls2 };
			var style = new CompositeLineStyle(ls);

			style.Enable = expectedEnable;
			style.Color = expectedColor;
			style.LineWidth = expectedWidth;

			ls1.Received(1).Color = expectedColor;
			ls1.Received(1).Enable = expectedEnable;
			ls1.Received(1).LineWidth = expectedWidth;
			ls2.Received(1).Color = expectedColor;
			ls2.Received(1).Enable = expectedEnable;
			ls2.Received(1).LineWidth = expectedWidth;
		}
	}
}