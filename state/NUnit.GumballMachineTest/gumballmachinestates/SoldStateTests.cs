using NUnit.Framework;
using System;
using GumBallMachineCommon;
using NSubstitute;

namespace GumBallMachineState.Tests
{
	[TestFixture()]
	[Parallelizable]
	public class SoldStateTests
	{
		private IGumballMachineFacade _machine;
		private IErrorHandler _errorHandler;
		private IActionsLogger _logger;
		private SoldState _state;
		private IStateMessages _errorMessages;
		private IStateMessages _succeededMessages;

		[SetUp]
		public void SetUp()
		{
			_errorHandler = Substitute.For<IErrorHandler>();
			_logger = Substitute.For<IActionsLogger>();
			_machine = Substitute.For<IGumballMachineFacade>();
			_errorMessages = new SoldStateFailedMessages();
			_succeededMessages = new SoldStateSucceededMessages();
			_state = new SoldState(_machine, _errorHandler, _logger);
		}

		[Test]
		public void InsertQuarterTest()
		{
			Assert.That(() => _state.InsertQuarter(), Throws.Nothing);
			_errorHandler.Received(1).InvalidAction(_errorMessages.InsertQuarterMessage);
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
			_errorHandler.Received(1).InvalidAction(_errorMessages.RefillMessage);
		}

		[Test]
		public void DispenseTest()
		{
			_machine.QuartersCount.Returns(1u);
			_machine.BallsCount.Returns(1u);
			Assert.That(() => _state.Dispense(), Throws.Nothing);
			_logger.Received(1).Log(_succeededMessages.DispenseMessage);
			_machine.Received(1).ReleaseBallAndWriteOffQuarter();
			_machine.Received(1).SetQuarterInsertedState();
		}

		[Test]
		public void DispenseTestNoQuartersLeft()
		{
			_machine.QuartersCount.Returns(0u);
			_machine.BallsCount.Returns(1u);
			Assert.That(() => _state.Dispense(), Throws.Nothing);
			_logger.Received(1).Log(_succeededMessages.DispenseMessage);
			_machine.Received(1).ReleaseBallAndWriteOffQuarter();
			_machine.Received(1).SetNoQuartersState();
		}

		[Test]
		public void DispenseTestNoQuartersNoBallsLeft()
		{
			_machine.QuartersCount.Returns(0u);
			_machine.BallsCount.Returns(0u);
			Assert.That(() => _state.Dispense(), Throws.Nothing);
			_logger.Received(1).Log(_succeededMessages.DispenseMessage);
			_machine.Received(1).ReleaseBallAndWriteOffQuarter();
			_machine.Received(1).SetSoldOutState();
		}

		[Test]
		public void TurnCrankTest()
		{
			Assert.That(() => _state.TurnCrank(), Throws.Nothing);
			_errorHandler.Received(1).InvalidAction(_errorMessages.TurnCrankMessage);
		}
	}
}