using NUnit.Framework;
using NSubstitute;
using command.document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;
using System.Collections;
using command.storage;

namespace command.document.Tests
{
	[TestFixture]
	[Parallelizable]
	public class ImageMementoTests
	{
		[TestCaseSource(typeof(Data), "TestCases")]
		public void ImageMementoTest(IImage image, IResolveConstraint constraint)
		{
			Assert.That(() => new ImageMemento(image, 0, 0), constraint);
		}

		[Test]
		public void RestoreTest()
		{
			var image = Substitute.For<IImage>();
			uint oldW = 10;
			uint oldH = 15;
			var memento = new ImageMemento(image, oldW, oldH);
			image.Resize(20, 25);
			image.ClearReceivedCalls();
			Assert.That(() => memento.Restore(), Throws.Nothing);
			image.Received(1).Resize(Arg.Is(oldW), Arg.Is(oldH));
		}

		class Data
		{
			public static IEnumerable TestCases
			{
				get
				{
					yield return new TestCaseData(null, Throws.ArgumentNullException);
					yield return new TestCaseData(Substitute.For<IImage>(), Throws.Nothing);
				}
			}
		}
	}
}