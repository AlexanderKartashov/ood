using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter.shapes
{
	public class RectangularPolygon : Shape
	{
		//private readonly Point _center;
		//private readonly uint _radius;
		//private readonly uint _vertexCount;
		private IList<Point> _points;

		public RectangularPolygon(Point? center, uint radius, uint vertexCount, Color color)
		{
			Color = color;
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
					(int)Math.Round(radius * Math.Cos(anglePart * i)),
					(int)Math.Round(radius * Math.Sin(anglePart * i))
				);
			}
		}

		protected override void DrawImpl(ICanvas canvas)
		{
			throw new NotImplementedException();
		}
	}
}
