using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ducks.Quack
{
	public class DuckQuack : IQuackBehaviuor
	{
		public DuckQuack(StringBuilder stringBuilder) => _stringBuilder = stringBuilder;

		public void Quack() => _stringBuilder?.Append("quack");

		private readonly StringBuilder _stringBuilder;
	}
}
