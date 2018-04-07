using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter_declarations
{
	public struct Point
	{
		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}

		public int X { get; private set; }
		public int Y { get; private set; }
	}
}
