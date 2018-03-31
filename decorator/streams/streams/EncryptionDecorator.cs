using System;

namespace streams
{
	public class EncryptionDecorator : IOutputDataStream
	{
		private readonly IOutputDataStream _stream;
		private readonly IEncryptionStrategy _encriptionStrategy;

		public EncryptionDecorator(IOutputDataStream stream,
			IEncryptionStrategy encriptionStrategy)
		{
			_stream = stream ?? throw new ArgumentNullException(nameof(stream));
			_encriptionStrategy = encriptionStrategy ?? throw new ArgumentNullException(nameof(encriptionStrategy));
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
				localData[i] = _encriptionStrategy.Encrypt(localData[i]);
			}
			_stream.WriteBlock(localData);
		}

		public void WriteByte(byte data)
		{
			_stream.WriteByte(_encriptionStrategy.Encrypt(data));			
		}
	}
}
