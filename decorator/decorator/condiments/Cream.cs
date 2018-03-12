using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator.condiments
{
	public class Cream : CondimentDecorator
	{
		protected override double CondimentCost => 25;

		protected override String CondimentDescription => "Cream";

		public Cream(IBeverage beverage)
			: base(beverage)
		{
		}
	}
}
