using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ducks.Fly
{
	public class FlyWithWings : IFlyBehaviuor
    {
		public FlyWithWings(StringBuilder stringBuilder) => _stringBuilder = stringBuilder;

		public void Fly() => _stringBuilder?.Append("fly with wings");

		private readonly StringBuilder _stringBuilder;
	}
}
