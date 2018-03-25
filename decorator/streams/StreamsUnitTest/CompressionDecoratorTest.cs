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
		[Test]
		public void TestWriteBlock()
		{
			var strategy = Substitute.For<ICompressionStrategy>();
			var stream = Substitute.For<IOutputDataStream>();

			byte[] data = Enumerable.Range(0, 100).Select(x => (byte)x).ToArray();
			strategy.Compress(Arg.Is(data)).Returns(data);

			var encription = new CompressionDecorator(stream, strategy);
			Assert.That(() => encription.WriteBlock(data), Throws.Nothing);

			stream.Received(1).WriteBlock(Arg.Is(data));
			stream.DidNotReceive().WriteByte(Arg.Any<byte>());
			strategy.Received(1).Compress(Arg.Is(data));
			strategy.DidNotReceive().Decompress(Arg.Any<byte[]>());
		}

		[Test]
		public void TestWriteByte()
		{
			var strategy = Substitute.For<ICompressionStrategy>();
			var stream = Substitute.For<IOutputDataStream>();

			var encription = new CompressionDecorator(stream, strategy);
			Assert.That(() => encription.WriteByte(0b0), Throws.Nothing);

			stream.Received(1).WriteByte(Arg.Is<byte>(0));
			stream.DidNotReceive().WriteBlock(Arg.Any<byte[]>());
			strategy.DidNotReceive().Compress(Arg.Any<byte[]>());
			strategy.DidNotReceive().Decompress(Arg.Any<byte[]>());
		}
	}
}
