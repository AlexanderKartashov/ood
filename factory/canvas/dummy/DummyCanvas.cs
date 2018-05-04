using painter.sdk;
using System.Text;

namespace canvas
{
	public class DummyCanvas : IVectorCanvas
	{
		private readonly StringBuilder _content = new StringBuilder();

		public DummyCanvas(int w, int h)
		{
			_content.AppendLine($"canvas. width {w}, height {h}");
		}

		public string Data => _content.ToString();

		public void Accept(ICanvasVisitor visitor)
		{
			visitor.Visit(this);
		}

		public void DrawEllipse(Point center, Point size)
		{
			_content.AppendLine($"DrawEllipse(Point ({center.X}, {center.Y}), Point ({size.X}, {size.Y})");
		}

		public void DrawLine(Point from, Point to)
		{
			_content.AppendLine($"DrawLine(Point ({from.X}, {from.Y}), Point ({to.X}, {to.Y})");
		}

		public void SetColor(Color color)
		{
			_content.AppendLine($"SetColor(Color {color.ToString()})");
		}
	}
}
