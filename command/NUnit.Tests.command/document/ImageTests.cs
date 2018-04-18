using NUnit.Framework;
using NSubstitute;
using command.document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using command.storage;
using NUnit.Framework.Constraints;
using System.Collections;

namespace command.document.Tests
{
	[TestFixture]
	[Parallelizable]
	public class ImageTests
	{
		[TestCaseSource(typeof(Data), "TestCases")]
		public void ImageTest(IResource res, IResolveConstraint constraint)
		{
			Assert.That(() => new Image(res, 0, 0), constraint);
		}

		[Test]
		public void ImageGettersTest()
		{
			uint w = 5;
			uint h = 10;
			var res = Substitute.For<IResource>();
			var image = new Image(res, w, h);
			//Assert.That(image.Name, Is.EqualTo(name));
			Assert.That(image.Width, Is.EqualTo(w));
			Assert.That(image.Height, Is.EqualTo(h));
		}

		[Test()]
		public void CreateMementoTest()
		{
			uint w = 5;
			uint h = 10;
			var res = Substitute.For<IResource>();
			var image = new Image(res, 0, 0);
			var memento = image.CreateMemento();
			Assert.That(() => image.Resize(w, h), Throws.Nothing);
			Assert.That(() => memento.Restore(), Throws.Nothing);
			Assert.That(image.Width, Is.EqualTo(0));
			Assert.That(image.Height, Is.EqualTo(0));
		}

		[Test()]
		public void ResizeTest()
		{
			uint w = 5;
			uint h = 10;
			var res = Substitute.For<IResource>();
			var image = new Image(res, 0, 0);
			Assert.That(() => image.Resize(w, h), Throws.Nothing);
			Assert.That(image.Width, Is.EqualTo(w));
			Assert.That(image.Height, Is.EqualTo(h));
		}

		class Data
		{
			public static IEnumerable TestCases
			{
				get
				{
					yield return new TestCaseData(null, Throws.ArgumentNullException);
					yield return new TestCaseData(Substitute.For<IResource>(), Throws.Nothing);
				}
			}
		}
	}
}