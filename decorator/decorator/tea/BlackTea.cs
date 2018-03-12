using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator.tea
{
	class BlackTea : Tea
	{
		public BlackTea(String description = "Black tea")
		: base(description)
		{
		}
	}
}
