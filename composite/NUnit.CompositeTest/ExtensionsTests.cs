using NUnit.Framework;
using composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PointI = composite.Point<int>;
using PointD = composite.Point<double>;

namespace composite.Tests
{
	[TestFixture()]
	[Parallelizable]
	public class ExtensionsTests
	{
		[Test()]
		public void NormalizeTest()
		{
			var rect = new Rect(0, 0, 10, 10);

			Assert.That(new PointI(5, 5).Normalize(rect), Is.EqualTo(new PointD(0.5, 0.5)));
			Assert.That(new PointI(0, 0).Normalize(rect), Is.EqualTo(new PointD(0, 0)));
			Assert.That(new PointI(-1, -1).Normalize(rect), Is.EqualTo(new PointD(0, 0)));
			Assert.That(new PointI(10, 10).Normalize(rect), Is.EqualTo(new PointD(1, 1)));
			Assert.That(new PointI(11, 11).Normalize(rect), Is.EqualTo(new PointD(1, 1)));
		}

		[Test()]
		public void DenormalizeTest()
		{
			var rect = new Rect(0, 0, 10, 10);

			Assert.That(new PointD(0, 0).Denormalize(rect), Is.EqualTo(new PointI(0, 0)));
			Assert.That(new PointD(1, 1).Denormalize(rect), Is.EqualTo(new PointI(10, 10)));
			Assert.That(new PointD(0.5, 0.5).Denormalize(rect), Is.EqualTo(new PointI(5, 5)));
		}
	}
}