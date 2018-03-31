using NSubstitute;
using NUnit.Framework;
using streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamsUnitTest
{
	[TestFixture]
	[Parallelizable]
	public class CompressionDecoratorTest
	{
		private  const int _maxCount = 127;

		[Test]
		public void TestWriteBlock()
		{
			var stream = Substitute.For<IOutputDataStream>();
			byte[] data = Enumerable.Repeat(0, 100).Select(x => (byte)x).ToArray();

			var encription = new RLECompressionDectorator(stream);
			Assert.That(() => encription.WriteBlock(data), Throws.Nothing);

			stream.DidNotReceive().WriteByte(Arg.Any<byte>());
			stream.DidNotReceive().WriteBlock(Arg.Any<byte[]>());

			Assert.That(() => encription.WriteByte(0), Throws.Nothing);
			stream.DidNotReceive().WriteByte(Arg.Any<byte>());
			stream.DidNotReceive().WriteBlock(Arg.Any<byte[]>());

			Assert.That(() => encription.WriteBlock(data), Throws.Nothing);
			stream.DidNotReceive().WriteByte(Arg.Any<byte>());
			stream.Received(1).WriteBlock(Arg.Is<byte[]>(x => x.SequenceEqual(new byte[] { _maxCount, 0 })));

			stream.ClearReceivedCalls();

			Assert.That(() => encription.WriteBlock(new byte[] { 5 }), Throws.Nothing);
			stream.DidNotReceive().WriteByte(Arg.Any<byte>());
			stream.Received(1).WriteBlock(Arg.Is<byte[]>(x => x.SequenceEqual(new byte[] { 74, 0 })));

			stream.ClearReceivedCalls();
			encription.Dispose();
			stream.DidNotReceive().WriteByte(Arg.Any<byte>());
			stream.Received(1).WriteBlock(Arg.Is<byte[]>(x => x.SequenceEqual(new byte[] { 1, 5 })));
		}

		[Test]
		public void TestWriteByte()
		{
			var stream = Substitute.For<IOutputDataStream>();

			var encription = new RLECompressionDectorator(stream);
			for (var i = 0; i < _maxCount; ++i)
			{
				Assert.That(() => encription.WriteByte(0), Throws.Nothing);

				stream.DidNotReceive().WriteByte(Arg.Any<byte>());
				stream.DidNotReceive().WriteBlock(Arg.Any<byte[]>());
			}

			Assert.That(() => encription.WriteByte(0), Throws.Nothing);
			stream.DidNotReceive().WriteByte(Arg.Any<byte>());
			stream.Received(1).WriteBlock(Arg.Is<byte[]>(x => x.SequenceEqual(new byte[] { _maxCount, 0 })));

			stream.ClearReceivedCalls();

			Assert.That(() => encription.WriteByte(1), Throws.Nothing);
			stream.DidNotReceive().WriteByte(Arg.Any<byte>());
			stream.Received(1).WriteBlock(Arg.Is<byte[]>(x => x.SequenceEqual(new byte[] { 1, 0 })));

			stream.ClearReceivedCalls();

			Assert.That(() => encription.WriteByte(0), Throws.Nothing);
			stream.DidNotReceive().WriteByte(Arg.Any<byte>());
			stream.Received(1).WriteBlock(Arg.Is<byte[]>(x => x.SequenceEqual(new byte[] { 1, 1 })));

			stream.ClearReceivedCalls();
			encription.Dispose();
			stream.DidNotReceive().WriteByte(Arg.Any<byte>());
			stream.Received(1).WriteBlock(Arg.Is<byte[]>(x => x.SequenceEqual(new byte[] { 1, 0 })));
		}
	}
}
