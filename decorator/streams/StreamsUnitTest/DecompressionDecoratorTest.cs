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
	public class DecompressionDecoratorTest
	{
		[Test]
		public void TestReadBlock()
		{
			var strategy = Substitute.For<ICompressionStrategy>();
			var stream = Substitute.For<IInputDataStream>();
			byte[] res = Enumerable.Range(0, 100).Select(x => (byte)x).ToArray();
			stream.ReadBlock(Arg.Is(100)).Returns(res);
			strategy.Decompress(Arg.Is(res)).Returns(res);

			var encription = new DecompressionDecorator(stream, strategy);
			Assert.That(() => encription.ReadBlock(100), Is.EqualTo(res));

			stream.DidNotReceive().ReadByte();
			stream.DidNotReceive().IsEOF();
			stream.Received(1).ReadBlock(Arg.Is(100));
			strategy.DidNotReceive().Compress(Arg.Any<byte[]>());
			strategy.Received(1).Decompress(Arg.Is(res));
		}

		[Test]
		public void TestReadByte()
		{
			var strategy = Substitute.For<ICompressionStrategy>();
			var stream = Substitute.For<IInputDataStream>();
			stream.ReadByte().Returns((byte)0);

			var encription = new DecompressionDecorator(stream, strategy);
			Assert.That(() => encription.ReadByte(), Is.EqualTo(0b0));

			stream.Received(1).ReadByte();
			stream.DidNotReceive().IsEOF();
			stream.DidNotReceive().ReadBlock(Arg.Any<int>());
			strategy.DidNotReceive().Compress(Arg.Any<byte[]>());
			strategy.DidNotReceive().Decompress(Arg.Any<byte[]>());
		}

		[Test]
		public void TestIsEOF([Values(true, false)] bool value)
		{
			var strategy = Substitute.For<ICompressionStrategy>();
			var stream = Substitute.For<IInputDataStream>();
			stream.IsEOF().Returns(value);

			var encription = new DecompressionDecorator(stream, strategy);
			Assert.That(() => encription.IsEOF(), Is.EqualTo(value));

			stream.Received(1).IsEOF();
			stream.DidNotReceive().ReadByte();
			stream.DidNotReceive().ReadBlock(Arg.Any<int>());
			strategy.DidNotReceive().Compress(Arg.Any<byte[]>());
			strategy.DidNotReceive().Decompress(Arg.Any<byte[]>());
		}
	}
}
