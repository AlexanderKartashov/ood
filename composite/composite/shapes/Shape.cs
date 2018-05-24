namespace composite
{
	public abstract class Shape : IShape
	{
		public virtual ILineStyle LineStyle { get; set; } = new LineStyle();
		public virtual IFillStyle FillStyle { get; set; } = new FillStyle();
		public virtual Rect Frame { get; set; }

		public virtual void Draw(ICanvas canvas)
		{
			BeforeDraw(canvas);
			DrawImpl(canvas);
			AfterDraw(canvas);
		}

		protected abstract void DrawImpl(ICanvas canvas);

		protected virtual void BeforeDraw(ICanvas canvas) { }

		protected virtual void AfterDraw(ICanvas canvas) { }
	}
}
