using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using streams;

namespace transform
{
	class StreamsPipeline
	{
		public void Pipeline(IInputDataStream input, IOutputDataStream output)
		{
			while(!input.IsEOF())
			{
				const int size = 1024 * 1024; // 1MB
				output.WriteBlock(input.ReadBlock(size));
			}
		}
	}
}
