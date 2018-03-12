using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator.condiments
{
	public class ChocolateLoubles : CondimentDecorator
	{
		private const uint MaxLoublesCount = 5;

		private readonly uint _lobules;

		protected override double CondimentCost => _lobules * 10;

		protected override string CondimentDescription => String.Format("{0} chocolade loubles", _lobules);

		public ChocolateLoubles(IBeverage beverage, uint lobules)
			: base(beverage) => _lobules = lobules >= MaxLoublesCount ? MaxLoublesCount : lobules;
	}
}
