using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator
{
	public class Capuccino : Beverage
	{
		public enum PortionType
		{
			Standard,
			Double
		}

		private readonly PortionType _portionType;

		public Capuccino(PortionType portionType)
		: base(description: "Capuccino") => _portionType = portionType;

		public override double Cost => (_portionType == PortionType.Standard ? 80 : 120);
	}
}
