using System;
using System.Collections.Generic;

using PointI = composite.Point<int>;
using PointD = composite.Point<double>;

namespace composite
{
	public class Triangle : Shape
	{
		private readonly PointD _vertex1;
		private readonly PointD _vertex2;
		private readonly PointD _vertex3;

		public Triangle(PointD vertex1, PointD vertex2, PointD vertex3)
		{
			_vertex1 = vertex1;
			_vertex2 = vertex2;
			_vertex3 = vertex3;
		}

		protected override void DrawImpl(ICanvas canvas)
		{
			var v1 = _vertex1.Denormalize(Frame);
			var v2 = _vertex2.Denormalize(Frame);
			var v3 = _vertex3.Denormalize(Frame);

			canvas.FillPolygon(new List<PointI>() { v1, v2, v3, v1 });

			canvas.MoveTo(v1);
			canvas.LineTo(v2);
			canvas.LineTo(v3);
			canvas.LineTo(v1);
		}
	}
}
