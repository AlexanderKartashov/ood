using graphics_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using modern_graphics_lib;

namespace adapters
{
	public class ObjectAdapter : ICanvas, IDisposable
	{
		private readonly ModernGraphicsRenderer _modernGraphicsRenderer;
		private Point _startPoint = new Point(0, 0);

		public ObjectAdapter(ModernGraphicsRenderer modernGraphicsRenderer)
		{
			_modernGraphicsRenderer = modernGraphicsRenderer ?? throw new ArgumentNullException(nameof(modernGraphicsRenderer));
			_modernGraphicsRenderer.BeginDraw();
		}

		public void Dispose()
		{
			_modernGraphicsRenderer.Dispose();
		}

		public void LineTo(int x, int y)
		{
			var endPoint = new Point(x, y);
			_modernGraphicsRenderer.DrawLine(_startPoint, endPoint);
			_startPoint = endPoint;
		}

		public void Moveto(int x, int y)
		{
			_startPoint = new Point(x, y);
		}
	}
}
