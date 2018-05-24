using System;
using System.Collections.Generic;

using PointI = composite.Point<int>;
using PointD = composite.Point<double>;

namespace composite
{
	public class Rectangle : SimpleShape
	{
		private readonly PointD _leftTop;
		private readonly PointD _rightBottom;

		public Rectangle(PointD leftTop, PointD rightBottom)
		{
			_leftTop = leftTop;
			_rightBottom = rightBottom;
		}

		protected override void DrawImpl(ICanvas canvas)
		{
			var lt = _leftTop.Denormalize(Frame);
			var rb = _rightBottom.Denormalize(Frame);

			var v1 = lt;
			var v2 = new PointI(rb.X, lt.Y);
			var v3 = rb;
			var v4 = new PointI(lt.X, rb.Y);

			canvas.FillPolygon(new List<PointI>() { v1, v2, v3, v4 });

			canvas.MoveTo(v1);
			canvas.LineTo(v2);
			canvas.LineTo(v3);
			canvas.LineTo(v4);
			canvas.LineTo(v1);
		}
	}
}
