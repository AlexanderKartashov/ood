using System.IO;
using System.Text;
using painter.sdk;

namespace canvas
{
	class SvgCanvas : IVectorCanvas
	{
		private Color _color = Color.Black;
		private StringBuilder _content = new StringBuilder();

		private readonly int _w;
		private readonly int _h;

		public string Data => _content.ToString();

		public SvgCanvas(int w, int h)
		{
			_w = w;
			_h = h;
		}

		public void DrawEllipse(Point leftTop, Point size)
		{
			Point radius = new Point(size.X / 2, size.Y / 2);
			Point center = new Point(leftTop.X + radius.X, leftTop.Y + radius.Y);
			_content.AppendLine($"<ellipse cx=\"{center.X}\" cy=\"{leftTop.Y}\" rx=\"{radius.X}\" ry=\"{radius.Y}\" stroke=\"{_color.ToString()}\" fill=\"{_color.ToString()}\"/>");
		}

		public void DrawLine(Point from, Point to)
		{
			_content.AppendLine($"<line x1=\"{from.X}\" y1=\"{from.Y}\" x2=\"{to.X}\" y2=\"{to.Y}\" stroke=\"{_color.ToString()}\"/>");
		}

		public void SetColor(Color color)
		{
			_color = color;
		}

		public void Accept(ICanvasVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
