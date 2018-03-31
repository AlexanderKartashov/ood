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
			var stream = Substitute.For<IInputDataStream>();
			stream.ReadBlock(Arg.Is(2)).Returns(new byte[] { 10, 0 }, new byte[] { 1, 1 }, new byte[] { 50, 100 });

			var expected = Enumerable.Repeat(0, 10).Concat(Enumerable.Repeat(1, 1).Concat(Enumerable.Repeat(100, 49))).ToArray();

			var encription = new RLEDecompressionDecorator(stream);
			Assert.That(() => encription.ReadBlock(60), Is.EqualTo(expected));

			stream.DidNotReceive().ReadByte();
			stream.Received(3).ReadBlock(Arg.Is(2));
		}

		[Test]
		public void TestReadByte()
		{
			var stream = Substitute.For<IInputDataStream>();
			stream.ReadBlock(Arg.Is(2)).Returns(new byte[] { 5, 0 });

			var encription = new RLEDecompressionDecorator(stream);
			Assert.That(() => encription.ReadByte(), Is.EqualTo(0));

			stream.DidNotReceive().ReadByte();
			stream.Received(1).ReadBlock(Arg.Is(2));
		}

		[Test]
		public void TestIsEOF([Values(true, false)] bool value)
		{
			var stream = Substitute.For<IInputDataStream>();
			stream.IsEOF().Returns(value);

			var encription = new RLEDecompressionDecorator(stream);
			Assert.That(() => encription.IsEOF(), Is.EqualTo(value));

			stream.Received(1).IsEOF();
			stream.DidNotReceive().ReadByte();
			stream.DidNotReceive().ReadBlock(Arg.Any<int>());

			stream.ClearReceivedCalls();

			stream.ReadBlock(2).Returns(new byte[] { 5, 0 });
			encription.ReadByte();
			Assert.That(() => encription.IsEOF(), Is.EqualTo(false));
			stream.Received(1).IsEOF();
		}
	}
}

