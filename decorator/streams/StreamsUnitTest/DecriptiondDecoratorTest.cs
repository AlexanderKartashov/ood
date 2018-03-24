using NUnit.Framework;
using NSubstitute;
using streams;
using System.Linq;

namespace StreamsUnitTest
{
	[TestFixture]
	[Parallelizable]
	public class DecriptiondDecoratorTest
	{
		[Test]
		public void TestDecriptInReadCall([Range(0, byte.MaxValue - 1)] byte i)
		{
			var strategy = Substitute.For<IEncriptionStrategy>();
			var stream = Substitute.For<IInputDataStream>();
			stream.ReadByte().Returns(i);
			strategy.Decript(Arg.Is(i)).Returns((byte)(i + 1));

			var encription = new DecriptionDecorator(stream, strategy);
			Assert.That(() => encription.ReadByte(), Is.EqualTo(i + 1));

			strategy.Received(1).Decript(i);
			strategy.DidNotReceive().Encript(Arg.Any<byte>());
			stream.Received(1).ReadByte();
			stream.DidNotReceive().ReadBlock(Arg.Any<int>());
		}

		public void TestDecriptInReadBlockCall()
		{
			var strategy = Substitute.For<IEncriptionStrategy>();
			var stream = Substitute.For<IInputDataStream>();

			byte[] bytes = Enumerable.Range(0, 100).Select(x => (byte)x).ToArray();
			stream.ReadBlock(Arg.Is(bytes.Length)).Returns(bytes);
			strategy.Decript(Arg.Any<byte>()).Returns(x => (byte)(x.Arg<byte>() + 1));

			byte[] expected = Enumerable.Range(1, 100).Select(x => (byte)x).ToArray();
			var encription = new DecriptionDecorator(stream, strategy);

			Assert.That(() => encription.ReadBlock(bytes.Length), Is.EquivalentTo(expected));

			strategy.Received(bytes.Length).Decript(Arg.Any<byte>());
			strategy.DidNotReceive().Encript(Arg.Any<byte>());
			stream.Received(1).ReadBlock(Arg.Is(bytes.Length));
			stream.DidNotReceive().ReadByte();
		}
	}
}
