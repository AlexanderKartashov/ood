using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator
{
	public class Lemon : CondimentDecorator
	{
		private readonly uint _quantity;

		protected override double CondimentCost => 10 * _quantity;

		protected override String CondimentDescription => String.Format("Lemon x {0}", _quantity);

		public Lemon(IBeverage beverage, uint quantity)
			: base(beverage) => _quantity = quantity;
	}
}
