using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace streams
{
	public class RLECompressionStrategy : ICompressionStrategy
	{
		public byte[] Compress(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException(nameof(bytes));
			}

			if (bytes.Length == 0)
			{
				return new byte[0];
			}

			Compressor compressor = new Compressor();
			return compressor.Compress(bytes);
		}

		public byte[] Decompress(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException(nameof(bytes));
			}

			if (bytes.Length == 0)
			{
				return new byte[0];
			}

			if (bytes.Length % 2 != 0)
			{
				throw new ArgumentException("bytes length must be even");
			}

			Decompressor decompressor = new Decompressor();
			return decompressor.Decompress(bytes);
		}

		class Compressor
		{
			class State
			{
				public byte Current { get; set; }
				public byte Count { get; set; }
			}

			private List<byte> _compressed = new List<byte>();
			private State _state = null;
			private const byte _maxCount = 127;

			public byte[] Compress(byte[] raw)
			{
				foreach(var rawByte in raw)
				{
					Add(rawByte);
				}
				return Compressed;
			}

			private void Add(byte value)
			{
				if (_state == null)
				{
					Init(value);
				}
				else
				{
					Update(value);
				}
			}

			private byte[] Compressed
			{
				get
				{
					Flush();
					return _compressed.ToArray();
				}
			}

			private void Init(byte value)
			{
				_state = new State { Count = 1, Current = value };
			}

			private void Update(byte value)
			{
				if ((_state.Current == value) && (_state.Count < _maxCount))
				{
					++_state.Count;
				}
				else
				{
					Flush();
					Init(value);
				}
			}

			private void Flush()
			{
				_compressed.Add(_state.Count);
				_compressed.Add(_state.Current);

				_state = null;
			}
		}

		class Decompressor
		{
			private List<byte> _decompressed = new List<byte>();

			public byte[] Decompress(byte[] bytes)
			{
				// length must be even
				for(var i = 0; i < bytes.Length; i = i + 2)
				{
					var count = bytes[i];
					var value = bytes[i + 1];

					_decompressed.AddRange(Enumerable.Repeat(value, count));
				}

				return Decompressed;
			}

			private byte[] Decompressed { get => _decompressed.ToArray(); }
		}
	}
}
