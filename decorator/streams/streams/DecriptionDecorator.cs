using System;

namespace streams
{
	public class DecriptionDecorator : IInputDataStream
	{
		private readonly IInputDataStream _stream;
		private readonly IEncriptionStrategy _encriptionStrategy;

		public DecriptionDecorator(IInputDataStream stream,
			IEncriptionStrategy encriptionStrategy)
		{
			_stream = stream ?? throw new ArgumentNullException(nameof(stream));
			_encriptionStrategy = encriptionStrategy ?? throw new ArgumentNullException(nameof(encriptionStrategy));
		}

		public bool IsEOF()
		{
			return _stream.IsEOF();
		}

		public byte[] ReadBlock(int size)
		{
			byte[] data = _stream.ReadBlock(size);
			for(var i = 0; i < data.Length; ++i)
			{
				data[i] = _encriptionStrategy.Decript(data[i]);
			}
			return data;
		}

		public byte ReadByte()
		{
			return _encriptionStrategy.Decript(_stream.ReadByte());
		}
	}
}
