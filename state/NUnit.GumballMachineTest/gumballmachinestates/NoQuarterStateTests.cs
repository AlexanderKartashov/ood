using NUnit.Framework;
using GumBallMachineCommon;
using NSubstitute;

namespace GumBallMachineState.Tests
{
	[TestFixture()]
	[Parallelizable]
	public class NoQuarterStateTests
	{
		private IGumballMachineFacade _machine;
		private IErrorHandler _errorHandler;
		private IActionsLogger _logger;
		private NoQuarterState _state;
		private IStateMessages _errorMessages;
		private IStateMessages _succeededMessages;

		[SetUp]
		public void SetUp()
		{
			_errorHandler = Substitute.For<IErrorHandler>();
			_logger = Substitute.For<IActionsLogger>();
			_machine = Substitute.For<IGumballMachineFacade>();
			_errorMessages = new NoQuarterStateFailedMessages();
			_succeededMessages = new NoQuarterStateSucceededMessages();
			_state = new NoQuarterState(_machine, _errorHandler, _logger);
		}

		[Test]
		public void InsertQuarterTest()
		{
			Assert.That(() => _state.InsertQuarter(), Throws.Nothing);
			_logger.Received(1).Log(Arg.Is(_succeededMessages.InsertQuarterMessage));
			_machine.Received(1).SetQuarterInsertedState();
		}

		[Test]
		public void EjectQuarterTest()
		{
			Assert.That(() => _state.EjectQuarter(), Throws.Nothing);
			_errorHandler.Received(1).InvalidAction(_errorMessages.EjectQuarterMessage);
		}

		[Test]
		public void TestRefill([Random(0u, uint.MaxValue, 1000)] uint balls)
		{
			Assert.That(() => _state.Refill(balls), Throws.Nothing);
			_logger.Received(1).Log(Arg.Is(_succeededMessages.RefillMessage));
			_machine.Received(1).Refill(balls);
		}

		[Test]
		public void DispenseTest()
		{
			Assert.That(() => _state.Dispense(), Throws.Nothing);
			_errorHandler.Received(1).InvalidAction(_errorMessages.DispenseMessage);
		}

		[Test]
		public void TurnCrankTest()
		{
			Assert.That(() => _state.TurnCrank(), Throws.Nothing);
			_errorHandler.Received(1).InvalidAction(_errorMessages.TurnCrankMessage);
		}
	}
}