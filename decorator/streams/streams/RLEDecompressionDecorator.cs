using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace streams
{
	public class RLEDecompressionDecorator : IInputDataStream
	{
		private RLECompressionState _state = null;
		private readonly IInputDataStream _stream;

		public RLEDecompressionDecorator(IInputDataStream stream) => _stream = stream ?? throw new ArgumentNullException(nameof(stream));

		public bool IsEOF()
		{
			return _stream.IsEOF() && _state == null;
		}

		public byte[] ReadBlock(int size)
		{
			var bytes = new List<byte>();
			for (var i = 0; i < size; ++i)
			{
				if (!_stream.IsEOF())
				{
					bytes.Add(ReadByte());
				}
				else
				{
					break;
				}
			}
			return bytes.ToArray();
		}

		public byte ReadByte()
		{
			if (_state == null)
			{
				_state = ReadNextState();
			}

			return ReadFromState();
		}

		private byte ReadFromState()
		{
			byte value = _state.Value;
			--_state.Count;
			if (_state.Count == 0)
			{
				_state = null;
			}
			return value;
		}

		private RLECompressionState ReadNextState()
		{
			byte[] state = _stream.ReadBlock(2);
			return new RLECompressionState {
				Count = state[0],
				Value = state[1]
			};
		}
	}
}
