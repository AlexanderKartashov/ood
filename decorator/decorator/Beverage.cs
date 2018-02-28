using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator
{
	public abstract class Beverage : IBeverage
	{
		private readonly string _description;

		public string Description => _description;

		public abstract double Cost { get; }

		protected Beverage(String description) => _description = description;
	}
}
