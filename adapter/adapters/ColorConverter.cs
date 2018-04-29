using modern_graphics_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adapters
{
	class ColorConverter
	{
		public static RGBAColor Convert(uint color)
		{
			return new RGBAColor(
				((color >> 16) & 0x0000FF) / byte.MaxValue,
				((color >> 8) & 0x0000FF) / byte.MaxValue,
				((color >> 0) & 0x0000FF) / byte.MaxValue,
				1.0f
			);			
		}
	}
}
