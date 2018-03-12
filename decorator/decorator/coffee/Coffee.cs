using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator.coffee
{
	public class Coffee : Beverage
	{
		public Coffee(String description = "Coffee")
			: base(description)
		{
		}

		public override double Cost => 60;
	}
}
