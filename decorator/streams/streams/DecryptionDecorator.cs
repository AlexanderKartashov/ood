using System;

namespace streams
{
	public class DecryptionDecorator : IInputDataStream
	{
		private readonly IInputDataStream _stream;
		private readonly IEncryptionStrategy _encriptionStrategy;

		public DecryptionDecorator(IInputDataStream stream,
			IEncryptionStrategy encriptionStrategy)
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
				data[i] = _encriptionStrategy.Decrypt(data[i]);
			}
			return data;
		}

		public byte ReadByte()
		{
			return _encriptionStrategy.Decrypt(_stream.ReadByte());
		}
	}
}
