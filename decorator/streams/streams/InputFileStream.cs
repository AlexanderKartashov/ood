using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace streams
{
	public class InputFileStream : IInputDataStream
	{
		public bool IsEOF()
		{
			throw new NotImplementedException();
		}

		public byte[] ReadBlock(int size)
		{
			throw new NotImplementedException();
		}

		public byte ReadByte()
		{
			throw new NotImplementedException();
		}
	}
}
