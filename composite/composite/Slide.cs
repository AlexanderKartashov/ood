using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace composite
{
	public class Slide : ISlide
	{
		public Point Size { get; private set; }

		public IShapes Shapes { get; } = new GroupShape();

		public void Draw(ICanvas canvas)
		{
			foreach(var shape in Shapes.Shapes)
			{
				shape.Draw(canvas);
			}
		}
	}
}
