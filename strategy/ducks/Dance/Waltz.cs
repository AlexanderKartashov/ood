using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ducks.Dance
{
	public class Waltz : IDanceBehaviuor
	{
		public Waltz(StringBuilder stringBuilder) => _stringBuilder = stringBuilder;

		public void Dance() => _stringBuilder?.Append("waltz");

		private readonly StringBuilder _stringBuilder;
	}
}
