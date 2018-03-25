using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using streams;

namespace StreamsUnitTest
{
	[TestFixture]
	[Parallelizable]
	class RLECompressionStrategyTest
	{
		[Test]
		public void TestNullArray()
		{
			RLECompressionStrategy compressor = new RLECompressionStrategy();
			Assert.That(() => compressor.Compress(null), Throws.ArgumentNullException);
		}

		[Test]
		public void TestEmptyArray()
		{
			RLECompressionStrategy compressor = new RLECompressionStrategy();
			Assert.That(() => compressor.Compress(new byte[0]), Is.EquivalentTo(new byte[0]));
		}

		[Test]
		public void TestCompressUniqueValues()
		{
			RLECompressionStrategy compressor = new RLECompressionStrategy();
			byte[] bytes = Enumerable.Range(0, 10).Select(x => (byte)x).ToArray();

			byte[] expected = new byte[20] {
				1, 0,
				1, 1,
				1, 2,
				1, 3,
				1, 4,
				1, 5,
				1, 6,
				1, 7,
				1, 8,
				1, 9
			};
			Assert.That(() => compressor.Compress(bytes), Is.EquivalentTo(expected));
		}

		[Test]
		public void TestRepeatedValues()
		{
			RLECompressionStrategy compressor = new RLECompressionStrategy();

			{
				byte[] bytes = Enumerable.Repeat(0, 10).Select(x => (byte)x).ToArray();
				byte[] expected = new byte[2] { 10, 0 };
				Assert.That(() => compressor.Compress(bytes), Is.EquivalentTo(expected));
			}

			{
				byte[] bytes = Enumerable.Repeat(0, 200).Select(x => (byte)x).ToArray();
				byte[] expected = new byte[4] { 127, 0, 73, 0 };
				Assert.That(() => compressor.Compress(bytes), Is.EquivalentTo(expected));
			}
		}
	}

	[TestFixture]
	[Parallelizable]
	class RLEDecompressionStrategyTest
	{
		[Test]
		public void TestNullArray()
		{
			RLECompressionStrategy compressor = new RLECompressionStrategy();
			Assert.That(() => compressor.Decompress(null), Throws.ArgumentNullException);
		}

		[Test]
		public void TestEmptyArray()
		{
			RLECompressionStrategy compressor = new RLECompressionStrategy();
			Assert.That(() => compressor.Decompress(new byte[0]), Is.EquivalentTo(new byte[0]));
		}

		[Test]
		public void TestOddLenghArray()
		{
			RLECompressionStrategy compressor = new RLECompressionStrategy();
			Assert.That(() => compressor.Decompress(new byte[1] { 0 }), Throws.ArgumentException);
		}

		[Test]
		public void TestDecompressUniqueValues()
		{
			RLECompressionStrategy compressor = new RLECompressionStrategy();

			byte[] bytes = new byte[20] {
				1, 0,
				1, 1,
				1, 2,
				1, 3,
				1, 4,
				1, 5,
				1, 6,
				1, 7,
				1, 8,
				1, 9
			};
			byte[] expected = Enumerable.Range(0, 10).Select(x => (byte)x).ToArray();
			Assert.That(() => compressor.Decompress(bytes), Is.EquivalentTo(expected));
		}

		[Test]
		public void TestRepeatedValues()
		{
			RLECompressionStrategy compressor = new RLECompressionStrategy();

			{
				byte[] bytes = new byte[2] { 10, 0 };
				byte[] expected = Enumerable.Repeat(0, 10).Select(x => (byte)x).ToArray();
				Assert.That(() => compressor.Decompress(bytes), Is.EquivalentTo(expected));
			}

			{
				byte[] bytes = new byte[4] { 127, 0, 73, 0 };
				byte[] expected = Enumerable.Repeat(0, 200).Select(x => (byte)x).ToArray();
				Assert.That(() => compressor.Decompress(bytes), Is.EquivalentTo(expected));
			}
		}
	}

	[TestFixture]
	[Parallelizable]
	class RLECompressionDecompressionStrategyTest
	{
		[Test]
		public void TestValues()
		{
			List<byte> list = new List<byte>();
			list.AddRange(Enumerable.Repeat(0, 10).Select(x => (byte)x));
			list.AddRange(Enumerable.Range(0, byte.MaxValue).Select(x => (byte)x));
			list.AddRange(Enumerable.Repeat(2, 200).Select(x => (byte)x));

			byte[] original = list.ToArray();

			RLECompressionStrategy compressor = new RLECompressionStrategy();

			byte[] compressed = null;
			Assert.That(() => { compressed = compressor.Compress(original); }, Throws.Nothing);

			byte[] decompressed = null;
			Assert.That(() => { decompressed = compressor.Decompress(compressed); }, Throws.Nothing);

			Assert.That(compressed, Is.Not.EquivalentTo(original));
			Assert.That(decompressed, Is.EquivalentTo(original));
		}
	}
}
