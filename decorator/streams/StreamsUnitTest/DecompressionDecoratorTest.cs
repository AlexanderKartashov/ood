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
	public class DedecompressionDecoratorTest
	{
		[Test]
		public void TestReadBlock()
		{
			var stream = Substitute.For<IInputDataStream>();
			stream.ReadBlock(Arg.Is(2)).Returns(new byte[] { 10, 0 }, new byte[] { 1, 1 }, new byte[] { 50, 100 });

			var expected = Enumerable.Repeat(0, 10).Concat(Enumerable.Repeat(1, 1).Concat(Enumerable.Repeat(100, 49))).ToArray();

			var decompression = new RLEDecompressionDecorator(stream);
			Assert.That(() => decompression.ReadBlock(60), Is.EqualTo(expected));

			stream.DidNotReceive().ReadByte();
			stream.Received(3).ReadBlock(Arg.Is(2));
		}

		[Test]
		public void TestReadByte()
		{
			var stream = Substitute.For<IInputDataStream>();
			stream.ReadBlock(Arg.Is(2)).Returns(new byte[] { 5, 0 });

			var decompression = new RLEDecompressionDecorator(stream);
			Assert.That(() => decompression.ReadByte(), Is.EqualTo(0));

			stream.DidNotReceive().ReadByte();
			stream.Received(1).ReadBlock(Arg.Is(2));
		}

		[Test]
		public void TestIsEOF([Values(true, false)] bool value)
		{
			var stream = Substitute.For<IInputDataStream>();
			stream.IsEOF().Returns(value);

			var decompression = new RLEDecompressionDecorator(stream);
			Assert.That(() => decompression.IsEOF(), Is.EqualTo(value));

			stream.Received(1).IsEOF();
			stream.DidNotReceive().ReadByte();
			stream.DidNotReceive().ReadBlock(Arg.Any<int>());

			stream.ClearReceivedCalls();

			stream.ReadBlock(2).Returns(new byte[] { 5, 0 });
			decompression.ReadByte();
			Assert.That(() => decompression.IsEOF(), Is.EqualTo(false));
			stream.Received(1).IsEOF();
		}
	}
}

