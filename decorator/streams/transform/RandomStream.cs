using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using streams;

namespace transform
{
	class RandomStream : IInputDataStream
	{
		private readonly Random _rand = new Random();

		public bool IsEOF()
		{
			return _rand.Next(0, int.MaxValue) > (int.MaxValue - (int.MaxValue / 10));
		}

		public byte[] ReadBlock(int size)
		{
			byte[] bytes = new byte[size];
			_rand.NextBytes(bytes);
			return bytes;
		}

		public byte ReadByte()
		{
			return (byte)_rand.Next(0, byte.MaxValue);
		}
	}
}
