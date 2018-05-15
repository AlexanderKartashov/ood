using System;
using System.Collections.Generic;
using System.Linq;

namespace composite
{
	internal class CompositeStylesUtils
	{
		public static T? GetCompositeValue<T, S>(Func<S, T?> getter, IEnumerable<S> styles) where T : struct
		{
			T? val = null;
			if (styles.Count() != 0)
			{
				val = getter(styles.First());
			}
			foreach (var style in styles)
			{
				val = GetNullableValue(val, getter(style));
			}
			return val;
		}

		public static T? GetNullableValue<T>(T? first, T? second) where T : struct
		{
			if ((first == null) || (second == null))
			{
				return null;
			}

			if (!first.Value.Equals(second.Value))
			{
				return null;
			}

			return first.Value;
		}
	}
}
