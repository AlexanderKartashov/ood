using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace streams
{
	interface IOutputDataStream
	{
		void WriteByte(byte data);

		//void WriteBlock(const void* srcData, long size) = 0;
	}
}
