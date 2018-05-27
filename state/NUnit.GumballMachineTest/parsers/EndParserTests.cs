using NUnit.Framework;

namespace GumBallMachineMenu.Tests
{
	[TestFixture()]
	[Parallelizable]
	public class EndParserTests
	{
		private EndParser _parser;

		[SetUp]
		public void Setup()
		{
			_parser = new EndParser();
		}

		[TestCase("")]
		[TestCase("something")]
		[TestCase("endr")]
		[TestCase("End ")]
		[TestCase(" end")]
		public void ParseFailedTest(string input)
		{
			dynamic result = null;
			Assert.That(() => result = _parser.Parse(input), Throws.Nothing);
			Assert.That(result, Is.Null);
		}

		[TestCase("End")]
		[TestCase("end")]
		[TestCase("END")]
		public void ParseSucceededTest(string input)
		{
			dynamic result = null;
			Assert.That(() => result = _parser.Parse(input), Throws.Nothing);
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Is.InstanceOf<End>());
		}
	}
}