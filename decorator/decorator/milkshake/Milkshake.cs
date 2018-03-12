using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator.milkshake
{
	public class Milkshake : Beverage
	{
		public Milkshake(String description = "Milkshake")
		: base(description)
		{
		}

		public override double Cost => 80;
	}
}
