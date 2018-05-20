using System.Collections.Generic;
using PointI = composite.Point<int>;

namespace composite
{
	public interface ICanvas
	{
		void SetLineColor(RGBAColor color);
		void SetLineWidth(uint width);
		void SetFillColor(RGBAColor color);

		void MoveTo(PointI point);
		void LineTo(PointI point);
		void FillPolygon(IEnumerable<PointI> points);

		void FillEllipse(PointI lt, PointI size);
		void DrawEllipse(PointI lt, PointI size);
	}
}