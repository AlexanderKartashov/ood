using System;

namespace composite
{
	public class Ellipse : Shape
	{
		private readonly Point _leftTop;
		private readonly Point _rightBottom;

		public Ellipse(Point leftTop, Point rightBottom)
		{
			_leftTop = leftTop;
			_rightBottom = rightBottom;
		}

		protected override void DrawImpl(ICanvas canvas)
		{
			var lt = _leftTop.Denormalize(Frame);
			var rb = _rightBottom.Denormalize(Frame);

			canvas.DrawEllipse(
				(int)(Math.Round(lt.X)), 
				(int)(Math.Round(lt.Y)),
				(int)(Math.Round(rb.X - lt.X)),
				(int)(Math.Round(rb.Y - lt.Y)));
		}
	}
}
