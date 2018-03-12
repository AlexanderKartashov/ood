using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator
{
	public abstract class CondimentDecorator : IBeverage
	{
		private readonly IBeverage _beverage;

		public String Description => String.Format("{0}, {1}", _beverage.Description, CondimentDescription);

		public double Cost => _beverage.Cost + CondimentCost;

		protected CondimentDecorator(IBeverage beverage) => _beverage = beverage;

		protected abstract String CondimentDescription { get; }
		protected abstract double CondimentCost { get; }
	}
}
