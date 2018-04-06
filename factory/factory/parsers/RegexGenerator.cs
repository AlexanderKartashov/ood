using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter.parsers
{
	public class RegexGenerator
	{
		public String Value { get; private set; }

		public void Start()
		{
			Value = string.Concat(Value, @"^");
		}

		public void End()
		{
			Value = string.Concat(Value, @"$");
		}

		public void ParseDigit(string value)
		{
			Value = string.Concat(Value, @"\s*?", value, @"{(\d+?)}");
		}

		public void ParseColor(string value)
		{
			Value = string.Concat(Value, @"\s*?", value, @"{(\w+?)}");
		}

		public void ParsePoint(string value)
		{
			Value = string.Concat(Value, @"\s*?", value, @"{(\d+?),\s*?(\d+?)}");
		}
	}
}
