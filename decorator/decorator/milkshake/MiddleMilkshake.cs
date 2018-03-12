using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator.milkshake
{
	class MiddleMilkshake : Milkshake
	{
		public MiddleMilkshake(String description = "Middle milkshake")
			: base(description)
		{
		}

		public override double Cost => 60;
	}
}
