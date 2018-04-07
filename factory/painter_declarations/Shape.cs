using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter_declarations
{
	public abstract class Shape
	{
		protected Color _color = Color.Black;

		public void Draw(ICanvas canvas)
		{
			canvas.SetColor(_color);
			DrawImpl(canvas);
		}

		protected abstract void DrawImpl(ICanvas canvas);
	}
}
