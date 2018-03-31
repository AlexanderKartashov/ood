using NUnit.Framework;
using NSubstitute;
using streams;
using System.Linq;

namespace StreamsUnitTest
{
	[TestFixture]
	[Parallelizable]
	public class DecryptiondDecoratorTest
	{
		[Test]
		public void TestReadByte([Range(0, byte.MaxValue - 1)] byte i)
		{
			var strategy = Substitute.For<IEncryptionStrategy>();
			var stream = Substitute.For<IInputDataStream>();
			stream.ReadByte().Returns(i);
			strategy.Decrypt(Arg.Is(i)).Returns((byte)(i + 1));

			var decryption = new DecryptionDecorator(stream, strategy);
			Assert.That(() => decryption.ReadByte(), Is.EqualTo(i + 1));

			strategy.Received(1).Decrypt(i);
			strategy.DidNotReceive().Encrypt(Arg.Any<byte>());
			stream.Received(1).ReadByte();
			stream.DidNotReceive().ReadBlock(Arg.Any<int>());
			stream.DidNotReceiveWithAnyArgs().IsEOF();
		}

		[Test]
		public void TestReadBlock()
		{
			var strategy = Substitute.For<IEncryptionStrategy>();
			var stream = Substitute.For<IInputDataStream>();

			byte[] bytes = Enumerable.Range(0, 100).Select(x => (byte)x).ToArray();
			stream.ReadBlock(Arg.Is(bytes.Length)).Returns(bytes);
			strategy.Decrypt(Arg.Any<byte>()).Returns(x => (byte)(x.Arg<byte>() + 1));

			byte[] expected = Enumerable.Range(1, 100).Select(x => (byte)x).ToArray();
			var decryption = new DecryptionDecorator(stream, strategy);

			Assert.That(() => decryption.ReadBlock(bytes.Length), Is.EquivalentTo(expected));

			strategy.Received(bytes.Length).Decrypt(Arg.Any<byte>());
			strategy.DidNotReceive().Encrypt(Arg.Any<byte>());
			stream.Received(1).ReadBlock(Arg.Is(bytes.Length));
			stream.DidNotReceive().ReadByte();
			stream.DidNotReceiveWithAnyArgs().IsEOF();
		}

		[Test]
		public void TestIsEOF([Values(true, false)] bool value)
		{
			var strategy = Substitute.For<IEncryptionStrategy>();
			var stream = Substitute.For<IInputDataStream>();
			stream.IsEOF().Returns(value);

			var decryption = new DecryptionDecorator(stream, strategy);
			Assert.That(() => decryption.IsEOF(), Is.EqualTo(value));

			stream.Received(1).IsEOF();
			stream.DidNotReceive().ReadByte();
			stream.DidNotReceive().ReadBlock(Arg.Any<int>());
			strategy.DidNotReceive().Encrypt(Arg.Any<byte>());
			strategy.DidNotReceive().Decrypt(Arg.Any<byte>());
		}
	}
}
