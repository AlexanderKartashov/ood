using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator
{
	public class IceCubes : CondimentDecorator
	{
		public enum IceCubeType
		{
			Dry,
			Water
		};

		private readonly uint _quantity;
		private readonly IceCubeType _type;

		protected override double CondimentCost => (_type == IceCubeType.Dry ? 10 : 5) * _quantity;

		protected override String CondimentDescription => String.Format("{0} ice cubes x {1}", (_type == IceCubeType.Dry ? "Dry" : "Water"), _quantity);

		public IceCubes(IBeverage beverage, uint quantity, IceCubeType type)
			: base(beverage)
		{
			_quantity = quantity;
			_type = type;
		}
	}
}
