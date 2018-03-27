using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter
{
	public class RectangularPolygon : Shape
	{
		private readonly Point _center;
		private readonly uint _radius;
		private readonly uint _vertexCount;

		public RectangularPolygon(Point? center, uint radius, uint vertexCount, Color color)
		{
			Color = color;
			_center = center ?? throw new ArgumentNullException(nameof(center));
			_radius = radius != 0 ? radius : throw new ArgumentException(nameof(radius));
			_vertexCount = vertexCount > 2 ? vertexCount : throw new ArgumentException(nameof(vertexCount));
		}

		protected override void DrawImpl(ICanvas canvas)
		{
			throw new NotImplementedException();
		}
	}
}
