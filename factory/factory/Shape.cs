using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter
{
	public abstract class Shape
	{
		public Color Color { get; protected set; } = Color.Black;

		public void Draw(ICanvas canvas)
		{
			canvas.SetColor(Color);
			DrawImpl(canvas);
		}

		protected abstract void DrawImpl(ICanvas canvas);
	}
}
