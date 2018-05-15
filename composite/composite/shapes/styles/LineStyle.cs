using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace composite
{
	public class LineStyle : Style, ILineStyle
	{
		public uint? LineWidth { get; set; } = null;
	}
}
