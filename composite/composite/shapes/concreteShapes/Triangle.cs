using System;

namespace composite
{
	public class Triangle : Shape
	{
		private readonly Point _vertex1;
		private readonly Point _vertex2;
		private readonly Point _vertex3;

		public Triangle(Point vertex1, Point vertex2, Point vertex3)
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

			canvas.MoveTo(
				(int)Math.Round(v1.X),
				(int)Math.Round(v1.Y));
			canvas.LineTo(
				(int)Math.Round(v2.X),
				(int)Math.Round(v2.Y));
			canvas.LineTo(
				(int)Math.Round(v3.X),
				(int)Math.Round(v3.Y));
		}
	}
}
