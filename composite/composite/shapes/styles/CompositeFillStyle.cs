using System;
using System.Collections.Generic;
using System.Linq;

namespace composite
{
	public class CompositeFillStyle : IFillStyle
	{
		private readonly IEnumerable<IFillStyle> _styles;

		public CompositeFillStyle(IEnumerable<IFillStyle> styles) => _styles = styles ?? throw new ArgumentNullException(nameof(styles));

		public RGBAColor? Color
		{
			get => CompositeStylesUtils.GetCompositeValue((x) => x.Color, _styles);
			set => _styles.ToList().ForEach(x => x.Color = value);
		}

		public bool? Enable
		{
			get => CompositeStylesUtils.GetCompositeValue((x) => x.Enable, _styles);
			set => _styles.ToList().ForEach(x => x.Enable = value);
		}
	}
}
