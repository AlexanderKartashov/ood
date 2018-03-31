using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace streams
{
	public class RLECompressionDectorator : IOutputDataStream
	{
		private RLECompressionState _state = null;
		private readonly IOutputDataStream _stream;
		private const byte _maxCount = 127;

		public RLECompressionDectorator(IOutputDataStream stream) => _stream = stream ?? throw new ArgumentNullException(nameof(stream));

		public void Dispose()
		{
			Flush();
			_stream.Dispose();
		}

		public void WriteBlock(byte[] data) => data.ToList().ForEach(value => WriteByte(value));

		public void WriteByte(byte data)
		{
			if (_state == null)
			{
				CreateState(data);
			}
			else
			{
				if (_state.Value == data)
				{
					if (_state.Count == _maxCount)
					{
						Flush();
					}
					else
					{
						++_state.Count;
						return;
					}
				}
				else
				{
					Flush();
				}

				CreateState(data);
			}
		}

		private void Flush()
		{
			if (_state != null)
			{
				byte[] data = new byte[] { _state.Count, _state.Value };
				_stream.WriteBlock(data);
				_state = null;
			}
		}

		private void CreateState(byte value)
		{
			_state = new RLECompressionState { Value = value };
		}
	}
}
