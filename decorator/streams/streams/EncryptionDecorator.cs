using System;

namespace streams
{
	public class EncryptionDecorator : IOutputDataStream
	{
		private readonly IOutputDataStream _stream;
		private readonly IEncryptionStrategy _encryptionStrategy;

		public EncryptionDecorator(IOutputDataStream stream,
			IEncryptionStrategy encryptionStrategy)
		{
			_stream = stream ?? throw new ArgumentNullException(nameof(stream));
			_encryptionStrategy = encryptionStrategy ?? throw new ArgumentNullException(nameof(encryptionStrategy));
		}

		public void Dispose()
		{
			_stream.Dispose();
		}

		public void WriteBlock(byte[] data)
		{
			byte[] localData = new byte[data.Length];
			data.CopyTo(localData, 0);
			for (var i = 0; i < localData.Length; ++i)
			{
				localData[i] = _encryptionStrategy.Encrypt(localData[i]);
			}
			_stream.WriteBlock(localData);
		}

		public void WriteByte(byte data)
		{
			_stream.WriteByte(_encryptionStrategy.Encrypt(data));			
		}
	}
}
