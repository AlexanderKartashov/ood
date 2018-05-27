using NUnit.Framework;

namespace GumBallMachineMenu.Tests
{
	[TestFixture()]
	[Parallelizable]
	public class StateParserTests
	{
		private StateParser _parser;

		[SetUp]
		public void Setup()
		{
			_parser = new StateParser();
		}

		[TestCase("")]
		[TestCase("something")]
		[TestCase("Stater")]
		[TestCase("State ")]
		[TestCase(" State")]
		public void ParseFailedTest(string input)
		{
			dynamic result = null;
			Assert.That(() => result = _parser.Parse(input), Throws.Nothing);
			Assert.That(result, Is.Null);
		}

		[TestCase("State")]
		[TestCase("state")]
		[TestCase("STATE")]
		public void ParseSucceededTest(string input)
		{
			dynamic result = null;
			Assert.That(() => result = _parser.Parse(input), Throws.Nothing);
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Is.InstanceOf<State>());
		}
	}
}