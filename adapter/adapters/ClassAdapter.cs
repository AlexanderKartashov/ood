using graphics_lib;
using modern_graphics_lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adapters
{
	public class ClassAdapter : modern_graphics_lib.ModernGraphicsRenderer,  ICanvas, IDisposable
	{
		private Point _startPoint = new Point(0, 0);
		private RGBAColor _currrentColor;

		public ClassAdapter(TextWriter textWriter)
			: base(textWriter)
		{
			BeginDraw();
		}

		public new void Dispose()
		{
			EndDraw();
		}

		public void LineTo(int x, int y)
		{
			var endPoint = new Point(x, y);
			DrawLine(_startPoint, endPoint, _currrentColor);
			_startPoint = endPoint;
		}

		public void Moveto(int x, int y)
		{
			_startPoint = new Point(x, y);
		}

		public void SetColor(uint rgbColor)
		{
			_currrentColor = ColorConverter.Convert(rgbColor);
		}
	}
}
