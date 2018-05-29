using NUnit.Framework;
using GumBallMachineCommon;
using NSubstitute;

namespace GumBallMachineState.Tests
{
	[TestFixture()]
	[Parallelizable]
	public class HasQuarterStateTests
	{
		private IGumballMachineFacade _machine;
		private IErrorHandler _errorHandler;
		private IActionsLogger _logger;
		private HasQuarterState _state;
		private IStateMessages _errorMessages;
		private IStateMessages _succeededMessages;

		[SetUp]
		public void SetUp()
		{
			_errorHandler = Substitute.For<IErrorHandler>();
			_logger = Substitute.For<IActionsLogger>();
			_machine = Substitute.For<IGumballMachineFacade>();
			_errorMessages = new HasQuarterStateFailedMessages();
			_succeededMessages = new HasQuarterStateSucceededMessages();
			_state = new HasQuarterState(_machine, _errorHandler, _logger);
		}

		[Test]
		public void TestInsertQuarterQuartersLimitReached()
		{
			_machine.QuartersCount.Returns(2u);
			_machine.QuartersLimit.Returns(2u);
			Assert.That(() => _state.InsertQuarter(), Throws.Nothing);
			_errorHandler.Received(1).InvalidAction(_errorMessages.InsertQuarterMessage);
		}

		[Test]
		public void TestInsertQuarter()
		{
			_machine.QuartersCount.Returns(2u);
			_machine.QuartersLimit.Returns(5u);
			Assert.That(() => _state.InsertQuarter(), Throws.Nothing);
			_logger.Log(_succeededMessages.InsertQuarterMessage);
			_machine.Received(1).InsertAdditionalQuarter();
		}

		[Test]
		public void TestEjectQuarter()
		{
			Assert.That(() => _state.EjectQuarter(), Throws.Nothing);
			_logger.Log(_succeededMessages.EjectQuarterMessage);
			_machine.Received(1).SetNoQuartersState();
		}

		[Test]
		public void TestTurnCrank()
		{
			_machine.BallsCount.Returns(1u);
			Assert.That(() => _state.TurnCrank(), Throws.Nothing);
			_logger.Received(1).Log(_succeededMessages.TurnCrankMessage);
			_machine.Received(1).SetSoldState();
		}

		[Test]
		public void TestTurnCrankNoBalls()
		{
			_machine.BallsCount.Returns(0u);
			Assert.That(() => _state.TurnCrank(), Throws.Nothing);
			_errorHandler.Received(1).InvalidAction(_errorMessages.TurnCrankMessage);
			_logger.DidNotReceive().Log(Arg.Any<string>());
		}

		[Test]
		public void TestDispense()
		{
			Assert.That(() => _state.Dispense(), Throws.Nothing);
			_errorHandler.Received(1).InvalidAction(_errorMessages.DispenseMessage);
		}

		[Test]
		public void TestRefill([Random(0u, uint.MaxValue, 1000)] uint balls)
		{
			_machine.BallsCount.Returns(0u);
			Assert.That(() => _state.Refill(balls), Throws.Nothing);
			_logger.Received(1).Log(_succeededMessages.RefillMessage);
			_machine.Received(1).Refill(balls);
		}
	}
}