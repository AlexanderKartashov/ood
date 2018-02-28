using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator
{
	public class Coffee : Beverage
	{
		public Coffee()
		: base(description: "Coffee")
		{
		}

		public override double Cost => 60;
	}
}
