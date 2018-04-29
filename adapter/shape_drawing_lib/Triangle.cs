using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using graphics_lib;

namespace shape_drawing_lib
{
	public class Triangle : ICanvasDrawable
	{
		private readonly Point _p1;
		private readonly Point _p2;
		private readonly Point _p3;
		private readonly uint _color;

		public Triangle(Point p1, Point p2, Point p3, uint color = 0x0)
		{
			_p1 = p1;
			_p2 = p2;
			_p3 = p3;
			_color = color;
		}

		public void Draw(ICanvas canvas)
		{
			canvas.SetColor(_color);
			canvas.Moveto(_p1.X, _p1.Y);
			canvas.LineTo(_p2.X, _p2.Y);
			canvas.LineTo(_p3.X, _p3.Y);
			canvas.LineTo(_p1.X, _p1.Y);
		}
	}
}
