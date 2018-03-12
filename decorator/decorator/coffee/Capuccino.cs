using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator.coffee
{
	public class Capuccino : Coffee
	{
		public Capuccino(String description = "Capuccino")
			: base(description)
		{
		}

		public override double Cost => 80;
	}
}
