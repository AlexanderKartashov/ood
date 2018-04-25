using graphics_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shape_drawing_lib
{
	public class CanvasPainter
	{
		private readonly ICanvas _canvas;

		public CanvasPainter(ICanvas canvas) => _canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));

		public void Draw(ICanvasDrawable drawable)
		{
			drawable.Draw(_canvas);
		}
	}
}
