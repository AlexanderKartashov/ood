using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator
{
	public class ChocolateCrumbs : CondimentDecorator
	{
		private readonly uint _mass;

		protected override double CondimentCost => 2 * _mass;

		protected override String CondimentDescription => String.Format("Chocolate crumbs {0} g", _mass);

		public ChocolateCrumbs(IBeverage beverage, uint mass)
			: base(beverage) => _mass = mass;
	}
}
