using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using streams;
using System.Collections;

namespace StreamsUnitTest
{
	[TestFixture]
	[Parallelizable]
	public class ReplacementTableGeneratorWithDifferentKeysTest
	{
		[Test]
		public void ReplacementTablesWithDiffetentKeysNotEqual([Random(1, int.MaxValue, 1000)] int i)
		{
			ReplacementTableGenerator generator = new ReplacementTableGenerator();
			var table1 = generator.GenerateReplacementTable(i);
			var table2 = generator.GenerateReplacementTable(i - 1);
			CollectionAssert.AreNotEquivalent(table1, table2);
		}
	}

	[TestFixture]
	[Parallelizable]
	public class ReplacementTableGeneratorWithEqualKeysTest
	{
		[Test]
		public void ReplacementTablesWithSameKeyAreEqual([Random(0, int.MaxValue, 1000)] int i)
		{
			ReplacementTableGenerator generator = new ReplacementTableGenerator();
			var table1 = generator.GenerateReplacementTable(i);
			var table2 = generator.GenerateReplacementTable(i);
			CollectionAssert.AreEquivalent(table1, table2);
		}
	}
}
