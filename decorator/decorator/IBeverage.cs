using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator
{
	public interface IBeverage
	{
		String Description { get; }
		double Cost { get; }
	}
}
