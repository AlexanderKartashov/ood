using System;

namespace streams
{
	public class DecryptionDecorator : IInputDataStream
	{
		private readonly IInputDataStream _stream;
		private readonly IEncryptionStrategy _encryptionStrategy;

		public DecryptionDecorator(IInputDataStream stream,
			IEncryptionStrategy encryptionStrategy)
		{
			_stream = stream ?? throw new ArgumentNullException(nameof(stream));
			_encryptionStrategy = encryptionStrategy ?? throw new ArgumentNullException(nameof(encryptionStrategy));
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
				data[i] = _encryptionStrategy.Decrypt(data[i]);
			}
			return data;
		}

		public byte ReadByte()
		{
			return _encryptionStrategy.Decrypt(_stream.ReadByte());
		}
	}
}
