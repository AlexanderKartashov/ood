using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace composite
{
	public interface IStyle
	{
		RGBAColor? Color { get; set; }
		bool? Enable { get; set; }
	}
}
