using NUnit.Framework;

namespace GumBallMachineMenu.Tests
{
	[TestFixture()]
	[Parallelizable]
	public class RefillParserTests
	{
		private RefillParser _parser;

		[SetUp]
		public void Setup()
		{
			_parser = new RefillParser();
		}

		[TestCase("")]
		[TestCase("something")]
		[TestCase("Refill")]
		[TestCase("Refill1")]
		[TestCase("Refill 1 ")]
		[TestCase(" Refill 1")]
		[TestCase("Refill 0")]
		public void ParseFailedTest(string input)
		{
			dynamic result = null;
			Assert.That(() => result = _parser.Parse(input), Throws.Nothing);
			Assert.That(result, Is.Null);
		}

		[TestCase("Refill 1")]
		[TestCase("refill 1")]
		[TestCase("REFILL 1")]
		public void ParseSucceededTest(string input)
		{
			dynamic result = null;
			Assert.That(() => result = _parser.Parse(input), Throws.Nothing);
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Is.InstanceOf<Refill>());
			Assert.That(((Refill)result).Balls, Is.EqualTo(1u));
		}
	}
}