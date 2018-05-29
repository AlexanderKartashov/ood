using NUnit.Framework;
using GumBallMachineCommon;
using NSubstitute;

namespace GumBallMachineState.Tests
{
	[TestFixture()]
	[Parallelizable]
	public class SoldOutStateTests
	{
		private IGumballMachineFacade _machine;
		private IErrorHandler _errorHandler;
		private IActionsLogger _logger;
		private SoldOutState _state;
		private IStateMessages _errorMessages;
		private IStateMessages _succeededMessages;

		[SetUp]
		public void SetUp()
		{
			_errorHandler = Substitute.For<IErrorHandler>();
			_logger = Substitute.For<IActionsLogger>();
			_machine = Substitute.For<IGumballMachineFacade>();
			_errorMessages = new SoldOutStateFailedMessages();
			_succeededMessages = new SoldOutStateSucceededMessages();
			_state = new SoldOutState(_machine, _errorHandler, _logger);
		}

		[Test]
		public void InsertQuarterTest()
		{
			Assert.That(() => _state.InsertQuarter(), Throws.Nothing);
			_errorHandler.Received(1).InvalidAction(_errorMessages.InsertQuarterMessage);
		}

		[Test]
		public void EjectQuarterNoQuartersTest()
		{
			_machine.QuartersCount.Returns(0u);
			Assert.That(() => _state.EjectQuarter(), Throws.Nothing);
			_errorHandler.Received(1).InvalidAction(_errorMessages.EjectQuarterMessage);
		}

		[Test]
		public void EjectQuarterTest()
		{
			_machine.QuartersCount.Returns(1u);
			Assert.That(() => _state.EjectQuarter(), Throws.Nothing);
			_logger.Received(1).Log(_succeededMessages.EjectQuarterMessage);
		}

		[Test]
		public void TestRefillNoQuarters()
		{
			_machine.QuartersCount.Returns(0u);
			Assert.That(() => _state.Refill(1u), Throws.Nothing);
			_logger.Received(1).Log(Arg.Is(_succeededMessages.RefillMessage));
			_machine.Received(1).Refill(1u);
			_machine.Received(1).SetNoQuartersState();
		}

		[Test]
		public void TestRefillQuartersInserted()
		{
			_machine.QuartersCount.Returns(1u);
			Assert.That(() => _state.Refill(1u), Throws.Nothing);
			_logger.Received(1).Log(Arg.Is(_succeededMessages.RefillMessage));
			_machine.Received(1).Refill(1u);
			_machine.Received(1).SetQuarterInsertedState();
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