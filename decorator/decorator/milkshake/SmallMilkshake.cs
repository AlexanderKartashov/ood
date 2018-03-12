using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator.milkshake
{
	public class SmallMilkshake : Milkshake
	{
		public SmallMilkshake(String description = "Small milkshake")
			:base(description)
		{
		}

		public override double Cost => 50;
	}
}
