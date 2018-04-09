using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace streams
{
	public class InputFileStream : IInputDataStream
	{
		private readonly BinaryReader _file;

		public InputFileStream(string filePath)
		{
			_file = new BinaryReader(new FileStream(filePath, FileMode.Open, FileAccess.Read), Encoding.ASCII);
		}

		public bool IsEOF()
		{
			return _file.IsEOF();
		}

		public byte[] ReadBlock(int size)
		{
			return _file.ReadBytes(size);
		}

		public byte ReadByte()
		{
			return _file.ReadByte();
		}
	}

	static class BinaryReaderExtension
	{
		public static bool IsEOF(this BinaryReader reader)
		{
			return reader.BaseStream.Length == reader.BaseStream.Position;
		}
	}
}
