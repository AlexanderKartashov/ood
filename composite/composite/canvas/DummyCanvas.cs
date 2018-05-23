using System.Collections.Generic;
using System.Linq;
using System.Text;

using PointI = composite.Point<int>;

namespace composite
{
	public class DummyCanvas : ICanvas
	{
		private readonly StringBuilder _sb = new StringBuilder();

		public DummyCanvas(PointI size)
		{
			_sb.AppendLine($"Slide size: {size.X}, {size.Y}");
		}

		public void DrawEllipse(PointI lt, PointI size)
		{
			_sb.AppendLine($"Draw ellipse: leftTop: {lt.X}, {lt.Y}, size: {size.X}, {size.Y}");
		}

		public void FillEllipse(PointI lt, PointI size)
		{
			_sb.AppendLine($"Fill ellipse: leftTop: {lt.X}, {lt.Y}, size: {size.X}, {size.Y}");
		}

		public void FillPolygon(IEnumerable<PointI> points)
		{
			_sb.AppendLine("Fill polygon:");
			points.ToList().ForEach(x => _sb.AppendLine($"\tPoint: {x.X}, {x.Y}"));
			_sb.AppendLine("End fill polygon");
		}

		public void LineTo(PointI point)
		{
			_sb.AppendLine($"Line to: leftTop: {point.X}, {point.Y}");
		}

		public void MoveTo(PointI point)
		{
			_sb.AppendLine($"Move to: leftTop: {point.X}, {point.Y}");
		}

		public void SetFillColor(RGBAColor color)
		{
			_sb.AppendLine($"Fill color: {color.ToRGBA()}");
		}

		public void SetLineColor(RGBAColor color)
		{
			_sb.AppendLine($"Line color: {color.ToRGBA()}");
		}

		public void SetLineWidth(uint width)
		{
			_sb.AppendLine($"Line width: {width}");
		}

		public string Data { get => _sb.ToString(); }
	}
}
