using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PointI = composite.Point<int>;

namespace composite
{
	public class BitmapCanvas : ICanvas
	{
		private readonly Bitmap _bitmap;
		private readonly Graphics _graphics;

		private Pen _stroke = new Pen(new SolidBrush(Color.Transparent), 0);
		private Brush _fill = new SolidBrush(Color.Transparent);
		private PointI _start = new PointI(0, 0);

		public BitmapCanvas(PointI size)
		{
			_bitmap = new Bitmap(size.X, size.Y);
			_graphics = Graphics.FromImage(_bitmap);
		}

		public void DrawEllipse(PointI lt, PointI size)
		{
			_graphics.DrawEllipse(_stroke, lt.X, lt.Y, size.X, size.Y);
		}

		public void FillEllipse(PointI lt, PointI size)
		{
			_graphics.FillEllipse(_fill, lt.X, lt.Y, size.X, size.Y);
		}

		public void MoveTo(PointI point)
		{
			_start = point;
		}

		public void LineTo(PointI point)
		{
			_graphics.DrawLine(_stroke, _start.X, _start.Y, point.X, point.Y);
			_start = point;
		}
		
		public void FillPolygon(IEnumerable<PointI> points)
		{
			_graphics.FillPolygon(_fill, points.ToList().Select(x => new Point(x.X, x.Y)).ToArray());
		}

		public void SetLineColor(RGBAColor color)
		{
			_stroke = new Pen(new SolidBrush(Color.FromArgb(color.ToARGB())), 1);
		}

		public void SetLineWidth(uint width)
		{
			_stroke.Width = width;
		}

		public void SetFillColor(RGBAColor color)
		{
			_fill = new SolidBrush(Color.FromArgb(color.ToARGB()));
		}
	}
}
