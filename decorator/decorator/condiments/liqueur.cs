using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator.condiments
{
	public class Liqueur : CondimentDecorator
	{
		public enum LiqueurType
		{
			Chocolade,
			Nut
		}

		private readonly LiqueurType _type;

		protected override double CondimentCost => 50;

		protected override string CondimentDescription => String.Format("{0} liqueur", _type == LiqueurType.Chocolade ? "Chocolade" : "Nut");

		public Liqueur(IBeverage beverage, LiqueurType type)
			: base(beverage) => _type = type;
	}
}
