using NUnit.Framework;

namespace GumBallMachineMenu.Tests
{
	[TestFixture()]
	[Parallelizable]
	public class TurnCrankParserTests
	{
		private TurnCrankParser _parser;

		[SetUp]
		public void Setup()
		{
			_parser = new TurnCrankParser();
		}

		[TestCase("")]
		[TestCase("something")]
		[TestCase("TurnCrankr")]
		[TestCase("TurnCrank ")]
		[TestCase(" Turncrank")]
		public void ParseFailedTest(string input)
		{
			dynamic result = null;
			Assert.That(() => result = _parser.Parse(input), Throws.Nothing);
			Assert.That(result, Is.Null);
		}

		[TestCase("TurnCrank")]
		[TestCase("turncrank")]
		[TestCase("TURNCRANK")]
		public void ParseSucceededTest(string input)
		{
			dynamic result = null;
			Assert.That(() => result = _parser.Parse(input), Throws.Nothing);
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Is.InstanceOf<TurnCrank>());
		}
	}
}