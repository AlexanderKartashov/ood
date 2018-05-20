namespace composite
{
	public abstract class Shape : IShape
	{
		public virtual ILineStyle LineStyle { get; set; } = new LineStyle();
		public virtual IFillStyle FillStyle { get; set; } = new FillStyle();
		public virtual Rect Frame { get; set; }

		public virtual void Draw(ICanvas canvas)
		{
			SetFillStyle(canvas);
			SetLineStyle(canvas);

			DrawImpl(canvas);
		}

		protected abstract void DrawImpl(ICanvas canvas);

		private void SetFillStyle(ICanvas canvas)
		{
			var fillStyle = FillStyle;

			canvas.SetFillColor((StyleEnabled(fillStyle) && fillStyle.Color.HasValue) ?
				fillStyle.Color.Value :
				new RGBAColor(0)
			);
		}

		private void SetLineStyle(ICanvas canvas)
		{
			var defaultColor = new RGBAColor(0, 0, 0, 0);
			var defaultW = 0u;
			var lineStyle = LineStyle;

			canvas.SetLineColor(lineStyle.Color != null ? (StyleEnabled(lineStyle) ? lineStyle.Color.Value : defaultColor) : defaultColor);
			canvas.SetLineWidth(lineStyle.LineWidth != null ? (StyleEnabled(lineStyle) ? lineStyle.LineWidth.Value : defaultW) : defaultW);
		}

		private bool StyleEnabled(IStyle style)
		{
			return (style != null) && style.Enable.HasValue && style.Enable.Value;
		}
	}
}
