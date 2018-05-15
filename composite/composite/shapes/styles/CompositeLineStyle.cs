using System;
using System.Collections.Generic;
using System.Linq;

namespace composite
{
	public class CompositeLineStyle : ILineStyle
	{
		private readonly IEnumerable<ILineStyle> _styles;

		public CompositeLineStyle(IEnumerable<ILineStyle> styles) => _styles = styles ?? throw new ArgumentNullException(nameof(styles));

		public uint? LineWidth
		{
			get => CompositeStylesUtils.GetCompositeValue((x) => x.LineWidth, _styles);
			set => _styles.ToList().ForEach(x => x.LineWidth = value);
		}

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
