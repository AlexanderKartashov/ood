using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using painter;
using painter.shapes;
using painter_declarations;

namespace canvas
{
	public class SvgCanvas : ICanvas
	{
		private Color _color = Color.Black;
		private StringBuilder _content = new StringBuilder();

		private readonly string _path;
		private readonly int _w;
		private readonly int _h;

		public SvgCanvas(string path, int w, int h)
		{
			_path = path ?? throw new ArgumentNullException(nameof(path));
			_w = w;
			_h = h;
		}

		public void DrawEllipse(Point center, Point size)
		{
			_content.AppendLine($"<ellipse cx=\"{center.X}\" cy=\"{center.Y}\" rx=\"{size.X}\" ry=\"{size.Y}\" stroke=\"{_color.ToString()}\" fill=\"{_color.ToString()}\"/>");
		}

		public void DrawLine(Point from, Point to)
		{
			_content.AppendLine($"<line x1=\"{from.X}\" y1=\"{from.Y}\" x2=\"{to.X}\" y2=\"{to.Y}\" stroke=\"{_color.ToString()}\"/>");
		}

		public void SetColor(Color color)
		{
			_color = color;
		}

		public void Dispose()
		{
			Save(_path);
		}

		private void Save(string path)
		{
			var template = "<!DOCTYPE html><html><body><svg width=\"{0}\" height=\"{1}\">{2}</svg></body></html>";
			var filePath = Path.Combine(path, "index.html");
			using (var file = File.CreateText(filePath))
			{
				file.Write(string.Format(template, _w, _h, _content.ToString()));
			}
		}
	}
}
