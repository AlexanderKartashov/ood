
using PointI = composite.Point<int>;

namespace composite
{
	public struct Rect
	{
		public Rect(PointI lt, PointI rb)
		{
			LeftTop = lt;
			RightBottom = rb;
		}

		public Rect(PointI lt, int w, int h)
		{
			LeftTop = lt;
			RightBottom = new PointI(lt.X + w, lt.Y + h);
		}

		public Rect(int l, int t, int w, int h)
		{
			LeftTop = new PointI(l, t);
			RightBottom = new PointI(l + w, t + h);
		}

		public PointI LeftTop { get; private set; }
		public PointI RightBottom { get; private set; }
		public PointI Size { get => new PointI(RightBottom.X - LeftTop.X, RightBottom.Y - LeftTop.Y); }
	}
}
