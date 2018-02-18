using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ducks_functional.Fly
{
	public class FlyWithWingsWithState
	{
		public static Action Fly(StringBuilder stringBuilder)
		{
			int count = 0;
			return () => { stringBuilder?.AppendFormat("fly with wings, fly count = {0}", ++count); };
		}
	}
}
