using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter
{
	public class Ellipse : Shape
	{
		private readonly Point _leftTop;
		private readonly Point _size;

		public Ellipse(Point? leftTop, Point? size, Color color)
		{
			Color = color;
			_leftTop = leftTop ?? throw new ArgumentNullException(nameof(leftTop));
			_size = size ?? throw new ArgumentNullException(nameof(size));
		}

		protected override void DrawImpl(ICanvas canvas)
		{
			canvas.DrawEllipse(_leftTop, _size);
		}
	}
}
