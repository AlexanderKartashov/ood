using System;

namespace streams
{
	public class EncriptionDecorator : IOutputDataStream
	{
		private readonly IOutputDataStream _stream;
		private readonly IEncriptionStrategy _encriptionStrategy;

		public EncriptionDecorator(IOutputDataStream stream,
			IEncriptionStrategy encriptionStrategy)
		{
			_stream = stream ?? throw new ArgumentNullException(nameof(stream));
			_encriptionStrategy = encriptionStrategy ?? throw new ArgumentNullException(nameof(encriptionStrategy));
		}

		public void WriteBlock(byte[] data)
		{
			byte[] localData = new byte[data.Length];
			data.CopyTo(localData, 0);
			for (var i = 0; i < localData.Length; ++i)
			{
				localData[i] = _encriptionStrategy.Encript(localData[i]);
			}
			_stream.WriteBlock(localData);
		}

		public void WriteByte(byte data)
		{
			_stream.WriteByte(_encriptionStrategy.Encript(data));			
		}
	}
}
