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
		private readonly BinaryWriter _file;

		public OutputFileStream(string filePath)
		{
			_file = new BinaryWriter(new FileStream(filePath, FileMode.Create), Encoding.ASCII);
		}

		public void Dispose()
		{
			// do nothing
		}

		public void WriteBlock(byte[] data)
		{
			_file.Write(data);
		}

		public void WriteByte(byte data)
		{
			_file.Write(data);
		}
	}
}
