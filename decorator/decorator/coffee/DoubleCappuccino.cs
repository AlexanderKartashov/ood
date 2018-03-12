using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator.coffee
{
	public class DoubleCappuccino : Capuccino
	{
		public DoubleCappuccino(String description = "Capuccino")
			: base(description)
		{
		}

		public override double Cost => 120;
	}
}
