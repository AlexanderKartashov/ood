using PointI = composite.Point<int>;

namespace composite
{
	public interface ISlide : IDrawable
	{
		PointI Size { get; }

		IShapes Shapes { get; }
	}
}
