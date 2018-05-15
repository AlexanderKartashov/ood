using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace composite
{
	public struct Rect
	{
		public Rect(Point lt, Point rb)
		{
			LeftTop = lt;
			RightBottom = rb;
		}

		public Rect(Point lt, double w, double h)
		{
			LeftTop = lt;
			RightBottom = new Point(lt.X + w, lt.Y + h);
		}

		public Rect(double l, double t, double r, double b)
		{
			LeftTop = new Point(l, t);
			RightBottom = new Point(r, b);
		}

		public Point LeftTop { get; private set; }
		public Point RightBottom { get; private set; }
		public Point Size { get => new Point(RightBottom.X - LeftTop.X, RightBottom.Y - LeftTop.Y); }
	}
}
