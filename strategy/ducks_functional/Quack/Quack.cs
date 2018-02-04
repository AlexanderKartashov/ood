using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ducks_functional.Quack
{
	public class DuckQuack
	{
		public static void Quack(StringBuilder stringBuilder) => stringBuilder?.Append("quack");
	}
}
