using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator
{
	public class CoconutFlakes : CondimentDecorator
	{
		private readonly uint _mass;

		protected override double CondimentCost => 1 * _mass;

		protected override String CondimentDescription => String.Format("Coconut flakes {0} g", _mass);

		public CoconutFlakes(IBeverage beverage, uint mass)
			: base(beverage) => _mass = mass;
	}
}
