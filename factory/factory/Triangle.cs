using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter
{
	public class Triangle : Shape
	{
		private readonly Point _vertex1;
		private readonly Point _vertex2;
		private readonly Point _vertex3;

		public Triangle(Point? vertex1, Point? vertex2, Point? vertex3, Color color)
		{
			Color = color;
			_vertex1 = vertex1 ?? throw new ArgumentNullException(nameof(vertex1));
			_vertex2 = vertex2 ?? throw new ArgumentNullException(nameof(vertex2));
			_vertex3 = vertex3 ?? throw new ArgumentNullException(nameof(vertex3));
		}

		protected override void DrawImpl(ICanvas canvas)
		{
			canvas.DrawLine(_vertex1, _vertex2);
			canvas.DrawLine(_vertex2, _vertex3);
			canvas.DrawLine(_vertex3, _vertex1);
		}
	}
}
