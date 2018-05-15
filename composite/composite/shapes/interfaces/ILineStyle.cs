using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace composite
{
	public interface ILineStyle : IStyle
	{
		uint? LineWidth { get; set; }
	}
}
