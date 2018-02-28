using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator
{
	public class Latte : Beverage
	{
		public enum PortionType
		{
			Standard,
			Double
		}

		private readonly PortionType _portionType;

		public Latte(PortionType portionType)
		: base(description: "Latte") => _portionType = portionType;

		public override double Cost => (_portionType == PortionType.Standard ? 90 : 130);
	}
}
