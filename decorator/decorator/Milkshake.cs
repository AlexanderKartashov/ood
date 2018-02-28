using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator
{
	public class Milkshake : Beverage
	{
		public Milkshake()
		: base(description: "Milkshake")
		{
		}

		public override double Cost => 80;
	}
}
