using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator
{
	public class Cinnamon : CondimentDecorator
	{
		protected override double CondimentCost => 20;

		protected override String CondimentDescription => "Cinnamon";

		public Cinnamon(IBeverage beverage)
			: base(beverage)
		{
		}
	}
}
