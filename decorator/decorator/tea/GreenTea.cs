using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator.tea
{
	class GreenTea : Tea
	{
		public GreenTea(String description = "Green tea")
		: base(description)
		{
		}
	}
}
