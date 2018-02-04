using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ducks.Dance
{
	public class Manuette : IDanceBehaviuor
	{
		public Manuette(StringBuilder stringBuilder) => _stringBuilder = stringBuilder;

		public void Dance() => _stringBuilder?.Append("manuette");

		private readonly StringBuilder _stringBuilder;
	}
}
