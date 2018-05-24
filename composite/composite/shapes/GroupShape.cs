using System;
using System.Collections.Generic;
using System.Linq;

namespace composite
{
	public class GroupShape : Shape, IGroupShape
	{
		private readonly IList<IShape> _shapes = new List<IShape>();

		public IEnumerable<IShape> Shapes => _shapes;

		public void Insert(IShape shape, int? position = null)
		{
			var pos = ((position != null) && (position.Value >= _shapes.Count))
				? position.Value
				: _shapes.Count;

			_shapes.Insert(pos, shape);
		}

		public void Remove(IShape shape) => _shapes.Remove(shape);

		protected override sealed void DrawImpl(ICanvas canvas) => _shapes.ToList().ForEach(x => x.Draw(canvas));

		public override ILineStyle LineStyle
		{
			get => new CompositeLineStyle(EnumerateLineStyles());

			set => new CompositeLineStyle(EnumerateLineStyles())
				{
					Color = value.Color,
					Enable = value.Enable,
					LineWidth = value.LineWidth
				};
		}

		public override IFillStyle FillStyle
		{
			get => new CompositeFillStyle(EnumerateFillStyles());

			set => new CompositeFillStyle(EnumerateFillStyles())
				{
					Color = value.Color,
					Enable = value.Enable
				};
		}

		public override Rect Frame
		{
			get => new CompositeFrame(Shapes).Frame;
			set => new CompositeFrame(Shapes) { Frame = value };
		}

		private IEnumerable<IFillStyle> EnumerateFillStyles() => _shapes.Select(x => x.FillStyle);

		private IEnumerable<ILineStyle> EnumerateLineStyles() => _shapes.Select(x => x.LineStyle);
	}
}
