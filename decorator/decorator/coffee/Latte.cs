using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator.coffee
{
	public class Latte : Coffee
	{
		public Latte(String description = "Latte")
			: base(description)
		{
		}

		public override double Cost => 90;
	}
}
