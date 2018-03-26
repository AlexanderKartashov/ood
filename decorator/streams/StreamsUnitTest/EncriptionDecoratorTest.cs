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
		public void TestWriteByte([Range(0, byte.MaxValue - 1)] byte i)
		{
			var strategy = Substitute.For<IEncryptionStrategy>();
			var stream = Substitute.For<IOutputDataStream>();
			strategy.Encrypt(Arg.Is(i)).Returns((byte)(i + 1));

			var encription = new EncryptionDecorator(stream, strategy);
			Assert.That(() => encription.WriteByte(i), Throws.Nothing);

			strategy.Received(1).Encrypt(i);
			strategy.DidNotReceive().Decrypt(Arg.Any<byte>());
			stream.Received(1).WriteByte(Arg.Is((byte)(i + 1)));
			stream.DidNotReceive().WriteBlock(Arg.Any<byte[]>());
		}

		[Test]
		public void TestWriteBlock()
		{
			var strategy = Substitute.For<IEncryptionStrategy>();
			var stream = Substitute.For<IOutputDataStream>();

			byte[] bytes = Enumerable.Range(0, 100).Select(x => (byte)x).ToArray();
			strategy.Encrypt(Arg.Any<byte>()).Returns(x => (byte)(x.Arg<byte>() + 1));

			byte[] expected = Enumerable.Range(1, 100).Select(x => (byte)x).ToArray();
			var encription = new EncryptionDecorator(stream, strategy);

			Assert.That(() => encription.WriteBlock(bytes), Throws.Nothing);

			// original data not changed
			byte[] originalBytes = Enumerable.Range(0, 100).Select(x => (byte)x).ToArray();
			Assert.That(bytes, Is.EquivalentTo(originalBytes));

			strategy.Received(bytes.Length).Encrypt(Arg.Any<byte>());
			strategy.DidNotReceive().Decrypt(Arg.Any<byte>());
			stream.Received(1).WriteBlock(Arg.Is<byte[]>(x => x.SequenceEqual(expected)));
			stream.DidNotReceive().WriteByte(Arg.Any<byte>());
		}
	}
}
