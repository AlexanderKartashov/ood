using painter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter.parsers
{
	public class ColorParser
	{
		static public Color Parse(String descr)
		{
			var parsedEnum = (Color)Enum.Parse(typeof(Color), descr, true);
			return Enum.IsDefined(typeof(Color), parsedEnum) ? parsedEnum : throw new ArgumentException("invalid enum value");
		}
	}
}
