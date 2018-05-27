using NUnit.Framework;

namespace GumBallMachineMenu.Tests
{
	[TestFixture()]
	[Parallelizable]
	public class HelpParserTests
	{
		private HelpParser _parser;

		[SetUp]
		public void Setup()
		{
			_parser = new HelpParser();
		}

		[TestCase("")]
		[TestCase("something")]
		[TestCase("Helpr")]
		[TestCase("Help ")]
		[TestCase(" Help")]
		public void ParseFailedTest(string input)
		{
			dynamic result = null;
			Assert.That(() => result = _parser.Parse(input), Throws.Nothing);
			Assert.That(result, Is.Null);
		}

		[TestCase("Help")]
		[TestCase("help")]
		[TestCase("HELP")]
		public void ParseSucceededTest(string input)
		{
			dynamic result = null;
			Assert.That(() => result = _parser.Parse(input), Throws.Nothing);
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Is.InstanceOf<Help>());
		}
	}
}