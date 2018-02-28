using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator
{
	public class Syrup : CondimentDecorator
	{
		public enum SyrupType
		{
			Chocolate,
			Maple,
		};

		private readonly SyrupType _type;

		protected override double CondimentCost => 15;

		protected override String CondimentDescription => String.Format("{0} syrup", (_type == SyrupType.Chocolate ? "Chocolate" : "Maple"));

		public Syrup(IBeverage beverage, SyrupType type)
			: base(beverage) => _type = type;
	}
}
