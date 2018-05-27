using NUnit.Framework;

namespace GumBallMachineMenu.Tests
{
	[TestFixture()]
	[Parallelizable]
	public class EjectQuartersParserTests
	{
		private EjectQuartersParser _parser;

		[SetUp]
		public void Setup()
		{
			_parser = new EjectQuartersParser();
		}

		[TestCase("")]
		[TestCase("something")]
		[TestCase("ejectQuartersr")]
		[TestCase("ejectQuarters ")]
		[TestCase(" ejectQuarters")]
		public void ParseFailedTest(string input)
		{
			dynamic result = null;
			Assert.That(() => result = _parser.Parse(input), Throws.Nothing);
			Assert.That(result, Is.Null);
		}

		[TestCase("ejectQuarters")]
		[TestCase("EjectQuarters")]
		[TestCase("EJECTQUARTERS")]
		public void ParseSucceededTest(string input)
		{
			dynamic result = null;
			Assert.That(() => result = _parser.Parse(input), Throws.Nothing);
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Is.InstanceOf<EjectQuarters>());
		}
	}
}