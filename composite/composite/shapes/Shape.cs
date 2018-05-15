namespace composite
{
	public abstract class Shape : IShape
	{
		public virtual ILineStyle LineStyle { get; set; } = new LineStyle();
		public virtual IFillStyle FillStyle { get; set; } = new FillStyle();
		public virtual Rect Frame { get; set; }

		public virtual void Draw(ICanvas canvas)
		{
			BeginFill(canvas);
			SetLineStyle(canvas);
			DrawImpl(canvas);
			EndFill(canvas);
		}

		protected abstract void DrawImpl(ICanvas canvas);

		protected void BeginFill(ICanvas canvas)
		{
			if (StyleEnabled(FillStyle) && FillStyle.Color.HasValue)
			{
				canvas.BeginFill(FillStyle.Color.Value);
			}
		}

		protected void EndFill(ICanvas canvas)
		{
			if (StyleEnabled(FillStyle) && FillStyle.Color.HasValue)
			{
				canvas.EndFill();
			}
		}

		protected void SetLineStyle(ICanvas canvas)
		{
			var defaultColor = new RGBAColor(0, 0, 0, 0);
			var defaultW = 0u;

			canvas.SetLineColor(LineStyle.Color != null ? (StyleEnabled(LineStyle) ? LineStyle.Color.Value : defaultColor) : defaultColor);
			canvas.SetLineWidth(LineStyle.LineWidth != null ? (StyleEnabled(LineStyle) ? LineStyle.LineWidth.Value : defaultW) : defaultW);
		}

		private bool StyleEnabled(IStyle style)
		{
			return (style != null) && style.Enable.HasValue && style.Enable.Value;
		}
	}
}
