using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace composite
{
	public class BitmapCanvas : ICanvas
	{
		private readonly Bitmap _bitmap = new Bitmap(0, 0);
		private readonly Graphics _graphics;

		private Pen _stroke;
		private Brush _fill;

		public BitmapCanvas()
		{
			_graphics = Graphics.FromImage(_bitmap);
		}

		public void BeginFill(RGBAColor color)
		{
			_fill = new SolidBrush(Color.FromArgb(color.ToARGB()));
		}

		public void DrawEllipse(int left, int top, int width, int height)
		{
			if (_fill != null)
			{
			}
			_graphics.DrawEllipse(_stroke, left, top, width, height);
		}

		public void EndFill()
		{
			_fill = null;
		}

		public void LineTo(int x, int y)
		{
			
		}

		public void MoveTo(int x, int y)
		{
			
		}

		public void SetLineColor(RGBAColor color)
		{
			_stroke = new Pen(new SolidBrush(Color.FromArgb(color.ToARGB())), 1);
		}

		public void SetLineWidth(uint width)
		{
			_stroke.Width = width;
		}
	}
}
