using NUnit.Framework;
using NSubstitute;
using command.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using command.document;
using NUnit.Framework.Constraints;
using System.Collections;
using command.storage;

namespace command.commands.Tests
{
	[TestFixture]
	[Parallelizable]
	public class ResizeImageTests
	{
		[TestCaseSource(typeof(Data), "TestCases")]
		public void ResizeImageTest(IImage img, IResolveConstraint constraint)
		{
			Assert.That(() => new ResizeImage(img, 0, 0), constraint);
		}

		[TestCase]
		public void TestExecute()
		{
			uint oldW = 10;
			uint newW = 20;
			uint oldH = 15;
			uint newH = 25;
			var image = new Image(Substitute.For<IResource>(), oldW, oldH);
			var command = new ResizeImage(image, newW, newH);
			Assert.That(() => command.Execute(), Throws.Nothing);
			Assert.That(image.Width, Is.EqualTo(newW));
			Assert.That(image.Height, Is.EqualTo(newH));
		}

		[TestCase]
		public void TestUnexecute()
		{
			uint oldW = 10;
			uint newW = 20;
			uint oldH = 15;
			uint newH = 25;
			var image = new Image(Substitute.For<IResource>(), oldW, oldH);
			var command = new ResizeImage(image, newW, newH);
			Assert.That(() => command.Execute(), Throws.Nothing);
			Assert.That(() => command.Unexecute(), Throws.Nothing);
			Assert.That(image.Width, Is.EqualTo(oldW));
			Assert.That(image.Height, Is.EqualTo(oldH));
		}

		[TestCase]
		public void TestDestroy()
		{
			uint oldW = 10;
			uint newW = 20;
			uint oldH = 15;
			uint newH = 25;
			var image = new Image(Substitute.For<IResource>(), oldW, oldH);
			var command = new ResizeImage(image, newW, newH);
			Assert.That(() => command.Execute(), Throws.Nothing);
			Assert.That(() => command.Dispose(), Throws.Nothing);
		}

		class Data
		{
			public static IEnumerable TestCases
			{
				get
				{
					yield return new TestCaseData(null, Throws.TypeOf<CommandError>());
					yield return new TestCaseData(Substitute.For<IImage>(), Throws.Nothing);
				}
			}
		}
	}
}