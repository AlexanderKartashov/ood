using painter.sdk;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace canvas
{
	class BitmapCanvas : IBitmapCanvas
	{
		private readonly Bitmap _bmp;
		private readonly Graphics _g;
		private Pen _currentPen = new Pen(System.Drawing.Color.Black);

		public BitmapCanvas(int w, int h)
		{
			_bmp = new Bitmap(w, h);
			_g = Graphics.FromImage(_bmp);
			_g.FillRectangle(new SolidBrush(System.Drawing.Color.White), 0, 0, w, h);
		}

		public Bitmap Data  
		{
			get
			{
				_g.Flush();
				return (Bitmap)_bmp.Clone();
			}
		}

		public void Accept(ICanvasVisitor visitor)
		{
			visitor.Visit(this);
		}

		public void DrawEllipse(painter.sdk.Point center, painter.sdk.Point size)
		{
			_g.DrawEllipse(_currentPen, center.X, center.Y, size.X, size.Y);
		}

		public void DrawLine(painter.sdk.Point from, painter.sdk.Point to)
		{
			_g.DrawLine(_currentPen, from.X, from.Y, to.X, to.Y);
		}

		public void SetColor(painter.sdk.Color color)
		{
			System.Drawing.Color newColor = System.Drawing.Color.Black;
			switch (color)
			{
				case painter.sdk.Color.Green:
					newColor = System.Drawing.Color.Green;
					break;
				case painter.sdk.Color.Red:
					newColor = System.Drawing.Color.Red;
					break;
				case painter.sdk.Color.Blue:
					newColor = System.Drawing.Color.Blue;
					break;
				case painter.sdk.Color.Yellow:
					newColor = System.Drawing.Color.Yellow;
					break;
				case painter.sdk.Color.Pink:
					newColor = System.Drawing.Color.Pink;
					break;
				case painter.sdk.Color.Black:
					newColor = System.Drawing.Color.Black;
					break;
				default:
					break;
			}

			_currentPen = new Pen(newColor);
		}
	}
}
