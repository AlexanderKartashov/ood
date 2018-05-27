using NUnit.Framework;

namespace GumBallMachineMenu.Tests
{
	[TestFixture()]
	[Parallelizable]
	public class InsertQuarterParserTests
	{
		private InsertQuarterParser _parser;

		[SetUp]
		public void Setup()
		{
			_parser = new InsertQuarterParser();
		}

		[TestCase("")]
		[TestCase("something")]
		[TestCase("InsertQuarterr")]
		[TestCase("InsertQuarter ")]
		[TestCase(" insertquarter")]
		public void ParseFailedTest(string input)
		{
			dynamic result = null;
			Assert.That(() => result = _parser.Parse(input), Throws.Nothing);
			Assert.That(result, Is.Null);
		}

		[TestCase("InsertQuarter")]
		[TestCase("insertquarter")]
		[TestCase("INSERTQUARTER")]
		public void ParseSucceededTest(string input)
		{
			dynamic result = null;
			Assert.That(() => result = _parser.Parse(input), Throws.Nothing);
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Is.InstanceOf<InsertQuarter>());
		}
	}
}