using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modern_graphics_lib
{
	public class Point
	{
		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}

		public int X { get; private set; } = 0;
		public int Y { get; private set; } = 0;
	}
}
