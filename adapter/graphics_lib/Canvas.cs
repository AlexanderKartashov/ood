using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphics_lib
{
	public class Canvas : ICanvas
	{
		private readonly TextWriter _textWriter;

		public Canvas(TextWriter textWriter) => _textWriter = textWriter ?? throw new ArgumentNullException(nameof(textWriter));

		public void LineTo(int x, int y)
		{
			_textWriter.WriteLine($"Move to {x}, {y}");
		}

		public void Moveto(int x, int y)
		{
			_textWriter.WriteLine($"Line to {x}, {y}");
		}

		public void SetColor(uint rgbColor)
		{
			byte r = (byte)((rgbColor >> 16) & 0x0000FF);
			byte g = (byte)((rgbColor >> 8)  & 0x0000FF);
			byte b = (byte)((rgbColor >> 0)  & 0x0000FF);
			_textWriter.WriteLine($"Color #{r:X2}{g:X2}{b:X2}");
		}
	}
}
