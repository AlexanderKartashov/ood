using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator.tea
{
	class OolongTea : Tea
	{
		public OolongTea(String description = "Oolong tea")
			: base(description)
		{
		}
	}
}
