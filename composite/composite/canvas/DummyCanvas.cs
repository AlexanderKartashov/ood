using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace composite
{
	public class DummyCanvas : ICanvas
	{
		private readonly TextWriter _textWriter;

		public DummyCanvas(TextWriter textWriter) => _textWriter = textWriter ?? throw new ArgumentNullException(nameof(textWriter));

		public void BeginFill(RGBAColor color)
		{
			_textWriter.WriteLine($"Begin fill: r={color.R}, g={color.G}, b={color.B}, a={color.A}");
		}

		public void DrawEllipse(int left, int top, int width, int height)
		{
			_textWriter.WriteLine($"Draw ellipse: left={left}, top={top}, width={width}, height={height}");
		}

		public void EndFill()
		{
			_textWriter.WriteLine($"End fill");
		}

		public void LineTo(int x, int y)
		{
			_textWriter.WriteLine($"Line to: x={x}, y={y}");
		}

		public void MoveTo(int x, int y)
		{
			_textWriter.WriteLine($"Move to: x={x}, y={y}");
		}

		public void SetLineColor(RGBAColor color)
		{
			_textWriter.WriteLine($"Line color: r={color.R}, g={color.G}, b={color.B}, a={color.A}");
		}

		public void SetLineWidth(uint width)
		{
			_textWriter.WriteLine($"Line width: width={width}");
		}
	}
}
