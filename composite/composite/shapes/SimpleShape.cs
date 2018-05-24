namespace composite
{
	public abstract class SimpleShape : Shape
	{
		protected override sealed void BeforeDraw(ICanvas canvas)
		{
			SetFillStyle(canvas);
			SetLineStyle(canvas);
		}

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

		private bool StyleEnabled(IStyle style) => (style != null) && style.Enable.HasValue && style.Enable.Value;
	}
}
