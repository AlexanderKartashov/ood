using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ducks.Quack
{
	public class Squeak : IQuackBehaviuor
	{
		public Squeak(StringBuilder stringBuilder) => _stringBuilder = stringBuilder;

		public void Quack() => _stringBuilder?.Append("squeak");

		private readonly StringBuilder _stringBuilder;
	}
}
