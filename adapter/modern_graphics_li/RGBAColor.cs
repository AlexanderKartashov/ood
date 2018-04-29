using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modern_graphics_lib
{
	public struct RGBAColor
	{
		public RGBAColor(float r = 0.0f, float g = 0.0f, float b = 0.0f, float a = 0.0f)
		{
			R = r;
			G = g;
			B = b;
			A = a;
		}

		public float R { get; private set; }
		public float G { get; private set; }
		public float B { get; private set; }
		public float A { get; private set; }
	}
}
