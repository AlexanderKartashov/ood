using NSubstitute;
using NUnit.Framework;
using streams;
using System.Linq;

namespace StreamsUnitTest
{
	[TestFixture]
	[Parallelizable]
	public class EncriptionDecoratorTest
	{
		[Test]
		public void TestEncriptInWriteCall([Range(0, byte.MaxValue - 1)] byte i)
		{
			var strategy = Substitute.For<IEncriptionStrategy>();
			var stream = Substitute.For<IOutputDataStream>();
			strategy.Encript(Arg.Is(i)).Returns((byte)(i + 1));

			var encription = new EncriptionDecorator(stream, strategy);
			Assert.That(() => encription.WriteByte(i), Throws.Nothing);

			strategy.Received(1).Encript(i);
			strategy.DidNotReceive().Decript(Arg.Any<byte>());
			stream.Received(1).WriteByte(Arg.Is((byte)(i + 1)));
			stream.DidNotReceive().WriteBlock(Arg.Any<byte[]>());
		}

		public void TestEncriptInWriteBlockCall()
		{
			var strategy = Substitute.For<IEncriptionStrategy>();
			var stream = Substitute.For<IOutputDataStream>();

			byte[] bytes = Enumerable.Range(0, 100).Select(x => (byte)x).ToArray();
			strategy.Encript(Arg.Any<byte>()).Returns(x => (byte)(x.Arg<byte>() + 1));

			byte[] expected = Enumerable.Range(1, 100).Select(x => (byte)x).ToArray();
			var encription = new EncriptionDecorator(stream, strategy);

			Assert.That(() => encription.WriteBlock(bytes), Throws.Nothing);

			strategy.Received(bytes.Length).Encript(Arg.Any<byte>());
			strategy.DidNotReceive().Decript(Arg.Any<byte>());
			stream.Received(1).WriteBlock(Arg.Is(expected));
			stream.DidNotReceive().WriteByte(Arg.Any<byte>());

			// original data not changed
			byte[] originalBytes = Enumerable.Range(0, 100).Select(x => (byte)x).ToArray();
			Assert.That(bytes, Is.EquivalentTo(originalBytes));
		}
	}
}
