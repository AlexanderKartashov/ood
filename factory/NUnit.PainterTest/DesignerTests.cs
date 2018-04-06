using NUnit.Framework;
using NSubstitute;
using painter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		[Test()]
		public void CreateDraftTest()
		{
			Assert.Fail();
		}
	}
}