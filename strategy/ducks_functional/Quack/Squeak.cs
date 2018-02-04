using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ducks_functional.Quack
{
	public class Squeak
	{
		public static void Quack(StringBuilder stringBuilder) => stringBuilder?.Append("squeak");
	}
}
