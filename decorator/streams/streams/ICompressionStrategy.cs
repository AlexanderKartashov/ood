using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace streams
{
	public interface ICompressionStrategy
	{
		byte[] Compress(byte[] bytes);

		byte[] Decompress(byte[] bytes);
	}
}
