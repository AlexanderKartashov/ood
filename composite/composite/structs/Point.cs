using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace composite
{
	public struct Point
	{
		public Point(double x, double y)
		{
			X = x;
			Y = y;
		}

		public double X { get; private set; }
		public double Y { get; private set; }
	}
}
