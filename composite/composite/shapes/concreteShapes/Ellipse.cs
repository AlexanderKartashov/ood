using PointI = composite.Point<int>;
using PointD = composite.Point<double>;

namespace composite
{
	public class Ellipse : SimpleShape
	{
		private readonly PointD _leftTop;
		private readonly PointD _rightBottom;

		public Ellipse(PointD leftTop, PointD rightBottom)
		{
			_leftTop = leftTop;
			_rightBottom = rightBottom;
		}

		protected override void DrawImpl(ICanvas canvas)
		{
			var lt = _leftTop.Denormalize(Frame);
			var rb = _rightBottom.Denormalize(Frame);
			var size = new PointI(rb.X - lt.X, rb.Y - lt.Y);

			canvas.FillEllipse(lt, size);
			canvas.DrawEllipse(lt, size);
		}
	}
}
