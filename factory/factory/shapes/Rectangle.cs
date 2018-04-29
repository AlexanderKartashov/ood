using painter.sdk;
using System;

namespace painter.shapes
{
	public class Rectangle : Shape
	{
		private readonly Point _leftTop;
		private readonly Point _rightBottom;

		public Rectangle(Point? leftTop, Point? rightBottom, Color color)
		{
			_color = color;
			_leftTop = leftTop ?? throw new ArgumentNullException(nameof(leftTop));
			_rightBottom = rightBottom ?? throw new ArgumentNullException(nameof(rightBottom));
		}

		protected override void DrawImpl(ICanvas canvas)
		{
			var rightTop = new Point(_rightBottom.X, _leftTop.Y);
			var leftBottom = new Point(_leftTop.X, _rightBottom.Y);

			canvas.DrawLine(_leftTop, rightTop);
			canvas.DrawLine(rightTop, _rightBottom);
			canvas.DrawLine(_rightBottom, leftBottom);
			canvas.DrawLine(leftBottom, _leftTop);
		}
	}
}
