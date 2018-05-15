using System;

namespace composite
{
	public class Rectangle : Shape
	{
		private readonly Point _leftTop;
		private readonly Point _rightBottom;

		public Rectangle(Point leftTop, Point rightBottom)
		{
			_leftTop = leftTop;
			_rightBottom = rightBottom;
		}

		protected override void DrawImpl(ICanvas canvas)
		{
			var lt = _leftTop.Denormalize(Frame);
			var rb = _rightBottom.Denormalize(Frame);

			canvas.MoveTo(
				(int)Math.Round(lt.X),
				(int)Math.Round(lt.Y));
			canvas.LineTo(
				(int)Math.Round(rb.X),
				(int)Math.Round(lt.Y));
			canvas.LineTo(
				(int)Math.Round(rb.X),
				(int)Math.Round(rb.Y));
			canvas.LineTo(
				(int)Math.Round(lt.X),
				(int)Math.Round(rb.Y));
			canvas.LineTo(
				(int)Math.Round(lt.X),
				(int)Math.Round(lt.Y));
		}
	}
}
