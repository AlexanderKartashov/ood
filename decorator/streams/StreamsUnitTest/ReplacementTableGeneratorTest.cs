using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using streams;

namespace StreamsUnitTest
{
	[TestFixture]
	public class ReplacementTableGeneratorTest
	{
		[Test]
		public void CompareReplacementTables()
		{
			ReplacementTableGenerator generator = new ReplacementTableGenerator();

			var table1 = generator.GenerateReplacementTable(100);
			var table2 = generator.GenerateReplacementTable(100);
			var table3 = generator.GenerateReplacementTable(99);

			CollectionAssert.AreEquivalent(table1, table2);
			CollectionAssert.AreNotEquivalent(table1, table3);
		}
	}
}
