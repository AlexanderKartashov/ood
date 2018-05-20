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

		public void DrawEllipse(Point<int> lt, Point<int> size)
		{
			throw new NotImplementedException();
		}

		public void FillEllipse(Point<int> lt, Point<int> size)
		{
			throw new NotImplementedException();
		}

		public void FillPolygon(IEnumerable<Point<int>> points)
		{
			throw new NotImplementedException();
		}

		public void LineTo(Point<int> point)
		{
			throw new NotImplementedException();
		}

		public void MoveTo(Point<int> point)
		{
			throw new NotImplementedException();
		}

		public void SetFillColor(RGBAColor color)
		{
			throw new NotImplementedException();
		}

		public void SetLineColor(RGBAColor color)
		{
			throw new NotImplementedException();
		}

		public void SetLineWidth(uint width)
		{
			throw new NotImplementedException();
		}
	}
}
