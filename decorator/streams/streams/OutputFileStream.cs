using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace streams
{
	public class OutputFileStream : IOutputDataStream
	{
		private readonly FileStream _file;

		public OutputFileStream(string filePath)
		{
			
		}

		public void WriteBlock(byte[] data)
		{
			throw new NotImplementedException();
		}

		public void WriteByte(byte data)
		{
			throw new NotImplementedException();
		}
	}
}
