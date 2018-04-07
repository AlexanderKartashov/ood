using painter_declarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter.shapes
{
	public class Ellipse : Shape
	{
		private readonly Point _center;
		private readonly Point _size;

		public Ellipse(Point? center, Point? size, Color color)
		{
			_color = color;
			_center = center ?? throw new ArgumentNullException(nameof(center));
			_size = size ?? throw new ArgumentNullException(nameof(size));
		}

		protected override void DrawImpl(ICanvas canvas)
		{
			canvas.DrawEllipse(_center, _size);
		}
	}
}
