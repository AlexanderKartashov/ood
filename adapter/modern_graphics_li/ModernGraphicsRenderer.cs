using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modern_graphics_lib
{
	public class ModernGraphicsRenderer : IDisposable
	{
		private readonly TextWriter _textWriter;
		private bool _isDrawing = false;

		public ModernGraphicsRenderer(TextWriter textWriter) => _textWriter = textWriter ?? throw new ArgumentNullException(nameof(textWriter));

		public void Dispose()
		{
			EndDraw();
		}

		public void BeginDraw()
		{
			if (_isDrawing)
			{
				throw new InvalidOperationException("Drawing has already begun");
			}
			_textWriter.WriteLine("<draw>");
			_isDrawing = true;
		}

		public void EndDraw()
		{
			if (!_isDrawing)
			{
				throw new InvalidOperationException("Drawing has not been started");
			}
			_textWriter.WriteLine("</draw>");
			_isDrawing = false;
		}

		public void DrawLine(Point start, Point end, RGBAColor color)
		{
			if (!_isDrawing)
			{
				throw new InvalidOperationException("DrawLine is allowed between BeginDraw()/EndDraw() only");
			}
			_textWriter.WriteLine($"  <line fromX={start.X} fromY={start.Y} toX={end.X} toY={end.Y}>");
			_textWriter.WriteLine($"    <color r={color.R:F2} g={color.G:F2} b={color.B:F2} a={color.A:F2}/>");
			_textWriter.WriteLine($"  </line>");
		}
	}
}
