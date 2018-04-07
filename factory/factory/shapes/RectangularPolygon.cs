using painter_declarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter.shapes
{
	public class RectangularPolygon : Shape
	{
		private IList<Point> _points = new List<Point>();

		public RectangularPolygon(Point? center, uint radius, uint vertexCount, Color color)
		{
			_color = color;
			CalculateVertices(
				center ?? throw new ArgumentNullException(nameof(center)),
				radius != 0 ? radius : throw new ArgumentException(nameof(radius)),
				vertexCount > 2 ? vertexCount : throw new ArgumentException(nameof(vertexCount))
			);
		}

		private void CalculateVertices(Point center, uint radius, uint vertexCount)
		{
			var anglePart = 360 / vertexCount;

			for (var i = 0; i < vertexCount; ++i)
			{
				var point = new Point(
					(int)Math.Round(radius * Math.Cos(anglePart * i * Math.PI / 180.0f)) + center.X,
					(int)Math.Round(radius * Math.Sin(anglePart * i * Math.PI / 180.0f)) + center.Y
				);
				_points.Add(point);
			}
		}

		protected override void DrawImpl(ICanvas canvas)
		{
			for(var i = 0; i < _points.Count - 1; ++i)
			{
				canvas.DrawLine(_points[i], _points[i + 1]);
			}
			canvas.DrawLine(_points[_points.Count - 1], _points[0]);
		}
	}
}
