using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace streams
{
	public class DecompressionDecorator : IInputDataStream
	{
		private readonly IInputDataStream _stream;
		private readonly ICompressionStrategy _compression;

		public DecompressionDecorator(IInputDataStream stream,
			ICompressionStrategy compression)
		{
			_stream = stream ?? throw new ArgumentNullException(nameof(stream));
			_compression = compression ?? throw new ArgumentNullException(nameof(compression));
		}

		public bool IsEOF()
		{
			return _stream.IsEOF();
		}

		public byte[] ReadBlock(int size)
		{
			return _compression.Decompress(_stream.ReadBlock(size));
		}

		public byte ReadByte()
		{
			// nothing to decompress in one byte
			return _stream.ReadByte();
		}
	}
}
