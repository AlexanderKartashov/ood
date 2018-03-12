using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator.coffee
{
	public class DoubleLatte : Latte
	{
		public DoubleLatte(String description = "Double latte")
			: base(description)
		{
		}

		public override double Cost => 130;
	}
}
