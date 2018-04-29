using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using graphics_lib;

namespace shape_drawing_lib
{
	public class Rectangle : ICanvasDrawable
	{
		private readonly Point _leftTop;
		private readonly Point _size;
		private readonly uint _color;

		public Rectangle(Point leftTop, Point size, uint color = 0x0)
		{
			_leftTop = leftTop;
			_size = size;
			_color = color;
		}

		private Point RightBottom { get => new Point { X = _leftTop.X + _size.X, Y = _leftTop.Y + _size.Y }; }

		public void Draw(ICanvas canvas)
		{
			canvas.SetColor(_color);
			canvas.Moveto(_leftTop.X, _leftTop.Y);
			canvas.LineTo(RightBottom.X, _leftTop.Y);
			canvas.LineTo(RightBottom.X, RightBottom.Y);
			canvas.LineTo(_leftTop.X, RightBottom.Y);
			canvas.LineTo(_leftTop.X, _leftTop.Y);
		}
	}
}
