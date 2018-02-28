using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator
{
	public class Tea : Beverage
	{
		public Tea()
		: base(description: "Tea")
		{
		}

		public override double Cost => 30;
	}
}
