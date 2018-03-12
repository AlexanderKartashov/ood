using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator.tea
{
	public class Tea : Beverage
	{
		public Tea(String description = "Tea")
		: base(description)
		{
		}

		public override double Cost => 30;
	}
}
