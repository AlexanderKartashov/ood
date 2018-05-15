using NUnit.Framework;
using NSubstitute;
using composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace composite.Tests
{
	[TestFixture()]
	[Parallelizable]
	public class CompositeFillStyleTests
	{
		[Test()]
		public void TestReturnNullForEmptyCollection()
		{
			IEnumerable<IFillStyle> fs = new List<IFillStyle>();
			var style = new CompositeFillStyle(fs);
			Assert.That(style.Color, Is.Null);
			Assert.That(style.Enable, Is.Null);
		}

		[Test()]
		public void TestReturnNullForCollectionWithNullValues()
		{
			var fs1 = Substitute.For<IFillStyle>();
			fs1.Color.Returns((RGBAColor?)null);
			fs1.Enable.Returns((bool?)null);
			var fs2 = Substitute.For<IFillStyle>();
			fs2.Color.Returns((RGBAColor?)null);
			fs2.Enable.Returns((bool?)null);

			IEnumerable <IFillStyle> fs = new List<IFillStyle>() { fs1, fs2 };
			var style = new CompositeFillStyle(fs);
			Assert.That(style.Color, Is.Null);
			Assert.That(style.Enable, Is.Null);
		}

		[Test()]
		public void TestReturnNullForCollectionNullValue()
		{
			var fs1 = Substitute.For<IFillStyle>();
			fs1.Color.Returns((RGBAColor?)null);
			fs1.Enable.Returns((bool?)null);
			var fs2 = Substitute.For<IFillStyle>();
			fs2.Color.Returns(new RGBAColor(0, 0, 0, 0));
			fs2.Enable.Returns(false);

			IEnumerable<IFillStyle> fs = new List<IFillStyle>() { fs1, fs2 };
			var style = new CompositeFillStyle(fs);
			Assert.That(style.Color, Is.Null);
			Assert.That(style.Enable, Is.Null);
		}

		[Test()]
		public void TestReturnNullForCollectionNotequalNotNulllValues()
		{
			var fs1 = Substitute.For<IFillStyle>();
			fs1.Color.Returns(new RGBAColor(1, 2, 3, 4));
			fs1.Enable.Returns(true);
			var fs2 = Substitute.For<IFillStyle>();
			fs2.Color.Returns(new RGBAColor(0, 0, 0, 0));
			fs2.Enable.Returns(false);

			IEnumerable<IFillStyle> fs = new List<IFillStyle>() { fs1, fs2 };
			var style = new CompositeFillStyle(fs);
			Assert.That(style.Color, Is.Null);
			Assert.That(style.Enable, Is.Null);
		}

		[Test()]
		public void TestReturnNotNullForCollectionEqualNotNulllValues()
		{
			var expectedColor = new RGBAColor(0, 1, 2, 3);
			var expectedEnable = false;
			var fs1 = Substitute.For<IFillStyle>();
			fs1.Color.Returns(expectedColor);
			fs1.Enable.Returns(expectedEnable);
			var fs2 = Substitute.For<IFillStyle>();
			fs2.Color.Returns(expectedColor);
			fs2.Enable.Returns(expectedEnable);

			IEnumerable<IFillStyle> fs = new List<IFillStyle>() { fs1, fs2 };
			var style = new CompositeFillStyle(fs);
			Assert.That(style.Color, Is.EqualTo(expectedColor));
			Assert.That(style.Enable, Is.EqualTo(expectedEnable));
		}

		[Test]
		public void TestSetters()
		{
			var expectedColor = new RGBAColor(0, 1, 2, 3);
			var expectedEnable = false;

			var fs1 = Substitute.For<IFillStyle>();
			var fs2 = Substitute.For<IFillStyle>();

			IEnumerable<IFillStyle> fs = new List<IFillStyle>() { fs1, fs2 };
			var style = new CompositeFillStyle(fs);

			style.Enable = expectedEnable;
			style.Color = expectedColor;

			fs1.Received(1).Color = expectedColor;
			fs1.Received(1).Enable = expectedEnable;
			fs2.Received(1).Color = expectedColor;
			fs2.Received(1).Enable = expectedEnable;
		}
	}
}