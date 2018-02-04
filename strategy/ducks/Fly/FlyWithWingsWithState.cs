using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ducks.Fly
{
	public class FlyWithWingsWithState : IFlyBehaviuor
	{
		private readonly StringBuilder _stringBuilder;
		private int _flysCount = 0;

		public FlyWithWingsWithState(StringBuilder stringBuilder) => _stringBuilder = stringBuilder;

		public void Fly()
		{
			_stringBuilder.AppendFormat("flies with wings, fly count = {0}", ++_flysCount);
		}
	}
}
