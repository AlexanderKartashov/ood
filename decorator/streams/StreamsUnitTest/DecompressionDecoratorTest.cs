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
		public void TestMethod()
		{
			// TODO: Add your test code here
			Assert.Fail("ReadBlock");
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
