using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace composite
{
	public class Style : IStyle
	{
		public RGBAColor? Color { get; set; } = null;
		public bool? Enable { get; set; } = null;
	}
}
