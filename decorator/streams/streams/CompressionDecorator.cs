using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace streams
{
	public class CompressionDecorator : IOutputDataStream
	{
		private readonly IOutputDataStream _stream;
		private readonly ICompressionStrategy _compression;

		public CompressionDecorator(IOutputDataStream stream,
			ICompressionStrategy compression)
		{
			_stream = stream ?? throw new ArgumentNullException(nameof(stream));
			_compression = compression ?? throw new ArgumentNullException(nameof(compression));
		}

		public void WriteBlock(byte[] data)
		{
			_stream.WriteBlock(_compression.Compress(data));
		}

		public void WriteByte(byte data)
		{
			// nothing to compress in one byte
			_stream.WriteByte(data);
		}
	}
}
