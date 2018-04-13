using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter_declarations
{
	public class DisposableCanvasDecorator : ICanvas, IDisposable
	{
		private readonly ICanvas _canvas;
		private readonly string _directory;

		public DisposableCanvasDecorator(ICanvas canvas, string directory)
		{
			_canvas = canvas;
			_directory = directory;
		}

		public void Dispose()
		{
			Save(_directory);
		}

		public void DrawEllipse(Point center, Point size)
		{
			_canvas?.DrawEllipse(center, size);
		}

		public void DrawLine(Point from, Point to)
		{
			_canvas?.DrawLine(from, to);
		}

		public void Save(string directory)
		{
			_canvas?.Save(directory);
		}

		public void SetColor(Color color)
		{
			_canvas?.SetColor(color);
		}
	}
}
