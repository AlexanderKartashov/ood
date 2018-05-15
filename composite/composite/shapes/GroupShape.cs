using System;
using System.Collections.Generic;
using System.Linq;

namespace composite
{
	public class GroupShape : Shape, IGroupShape
	{
		private readonly IList<IShape> _shapes = new List<IShape>();

		public IEnumerable<IShape> Shapes => _shapes;

		//public int Count => _shapes.Count;

		public void Insert(IShape shape, int? position = null)
		{
			if ((position != null) && (position.Value >= _shapes.Count))
			{
				_shapes.Insert(position.Value, shape);
			}
			else
			{
				_shapes.Insert(_shapes.Count, shape);
			}
		}

		public void Remove(IShape shape) => _shapes.Remove(shape);

		public override void Draw(ICanvas canvas) => DrawImpl(canvas);

		protected override void DrawImpl(ICanvas canvas) => _shapes.ToList().ForEach(x => x.Draw(canvas));

		public override ILineStyle LineStyle
		{
			get => new CompositeLineStyle(EnumerateLineStyles());

			set
			{
				var ls = new CompositeLineStyle(EnumerateLineStyles());
				ls.Color = value.Color;
				ls.Enable = value.Enable;
				ls.LineWidth = value.LineWidth;
			}
		}

		public override IFillStyle FillStyle
		{
			get => new CompositeFillStyle(EnumerateFillStyles());

			set
			{
				var fs = new CompositeFillStyle(EnumerateFillStyles());
				fs.Color = value.Color;
				fs.Enable = value.Enable;
			}
		}

		public override Rect Frame
		{
			get
			{
				Rect result = new Rect(0, 0, 0, 0);

				foreach (var shape in Shapes)
				{
					result = shape.Frame.Union(result);
				}

				return result;
			}
			set
			{
				var currentRect = Frame;

				foreach (var shape in Shapes)
				{
					throw new NotImplementedException();
				}
			}
		}

		private IEnumerable<IFillStyle> EnumerateFillStyles()
		{
			return _shapes.Select(x => x.FillStyle);
		}

		private IEnumerable<ILineStyle> EnumerateLineStyles()
		{
			return _shapes.Select(x => x.LineStyle);
		}

	}
}
