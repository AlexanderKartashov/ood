using painter.sdk;
using System;

namespace painter.parsers
{
	public class ColorParser
	{
		static public Color Parse(String descr)
		{
			var parsedEnum = (Color)Enum.Parse(typeof(Color), descr, true);
			return Enum.IsDefined(typeof(Color), parsedEnum) ? parsedEnum : throw new ArgumentException($"invalid color value {descr}");
		}
	}
}
