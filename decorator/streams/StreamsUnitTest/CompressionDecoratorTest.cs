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
		public void TestMethod()
		{
			// TODO: Add your test code here
			Assert.Fail("WriteBlock");
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
