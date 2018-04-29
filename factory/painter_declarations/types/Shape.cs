namespace painter.sdk
{
	public abstract class Shape
	{
		protected Color _color = Color.Black;

		public void Draw(ICanvas canvas)
		{
			canvas.SetColor(_color);
			DrawImpl(canvas);
		}

		protected abstract void DrawImpl(ICanvas canvas);
	}
}
