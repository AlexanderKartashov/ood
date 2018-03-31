using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace streams
{
	class RLECompressionState
	{
		public byte Count { get; set; } = 1;
		public byte Value { get; set; } = 0;
	}
}
