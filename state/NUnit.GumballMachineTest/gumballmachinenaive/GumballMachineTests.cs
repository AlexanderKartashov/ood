using NUnit.Framework;
using System;
using GumBallMachineCommon;
using NSubstitute;

namespace NaiveGumballMachine.Tests
{
	[TestFixture()]
	public class GumballMachineTests
	{
		private IErrorHandler _errorHandler;
		private IActionsLogger _actionsLogger;

		[SetUp]
		public void SetUp()
		{
			_errorHandler = Substitute.For<IErrorHandler>();
			_actionsLogger = Substitute.For<IActionsLogger>();
		}
		
		#region refill
		[Test]
		public void RefillSoldOutTest()
		{
			var stateMachine = new GumBallMachineState.GumballMachine(0, 5, _errorHandler, _actionsLogger);
			var nativeMachine = new NaiveGumballMachine.GumballMachine(0, 5, _errorHandler, _actionsLogger);

			void check(IGumballMachineMaintenance machine) {
				Assert.That(() => machine.RefillBalls(5), Throws.Nothing);
				_actionsLogger.Received(1).Log(Arg.Any<string>());
				_errorHandler.DidNotReceive().InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();
			}

			DoActiionWithMachine(stateMachine, check);
			DoActiionWithMachine(nativeMachine, check);
		}

		[Test]
		public void RefillSoldOutAfterTurnCrankTest()
		{
			var stateMachine = new GumBallMachineState.GumballMachine(1, 5, _errorHandler, _actionsLogger);
			var nativeMachine = new NaiveGumballMachine.GumballMachine(1, 5, _errorHandler, _actionsLogger);

			void check(IGumballMachineMaintenance maintenance, IGumballMachine machine)
			{
				Assert.That(() => machine.InsertQuarter(), Throws.Nothing);
				Assert.That(() => machine.TurnCrank(), Throws.Nothing);

				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();

				Assert.That(() => maintenance.RefillBalls(5), Throws.Nothing);

				_actionsLogger.Received(1).Log(Arg.Any<string>());
				_errorHandler.DidNotReceive().InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();
			}

			DoActiionWithMachine(stateMachine, stateMachine, check);
			DoActiionWithMachine(nativeMachine, nativeMachine, check);
		}

		[Test]
		public void RefillNoQuarterTest()
		{
			var stateMachine = new GumBallMachineState.GumballMachine(1, 5, _errorHandler, _actionsLogger);
			var nativeMachine = new NaiveGumballMachine.GumballMachine(1, 5, _errorHandler, _actionsLogger);

			void check(IGumballMachineMaintenance maintenance)
			{
				Assert.That(() => maintenance.RefillBalls(5), Throws.Nothing);

				_actionsLogger.Received(1).Log(Arg.Any<string>());
				_errorHandler.DidNotReceive().InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();
			}

			DoActiionWithMachine(stateMachine, check);
			DoActiionWithMachine(nativeMachine, check);
		}

		[Test]
		public void RefillHasQuarterTest()
		{
			var stateMachine = new GumBallMachineState.GumballMachine(1, 5, _errorHandler, _actionsLogger);
			var nativeMachine = new NaiveGumballMachine.GumballMachine(1, 5, _errorHandler, _actionsLogger);

			void check(IGumballMachineMaintenance maintenance, IGumballMachine machine)
			{
				Assert.That(() => machine.InsertQuarter(), Throws.Nothing);

				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();

				Assert.That(() => maintenance.RefillBalls(5), Throws.Nothing);

				_actionsLogger.Received(1).Log(Arg.Any<string>());
				_errorHandler.DidNotReceive().InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();
			}

			DoActiionWithMachine(stateMachine, stateMachine, check);
			DoActiionWithMachine(nativeMachine, nativeMachine, check);
		}
		#endregion

		#region InsertQuarter
		[Test()]
		public void InsertQuarterNoQuarterTest()
		{
			var stateMachine = new GumBallMachineState.GumballMachine(1, 5, _errorHandler, _actionsLogger);
			var nativeMachine = new NaiveGumballMachine.GumballMachine(1, 5, _errorHandler, _actionsLogger);

			void check(IGumballMachine machine)
			{
				Assert.That(() => machine.InsertQuarter(), Throws.Nothing);

				_actionsLogger.Received(1).Log(Arg.Any<string>());
				_errorHandler.DidNotReceive().InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();
			}

			DoActiionWithMachine(stateMachine, check);
			DoActiionWithMachine(nativeMachine, check);
		}

		[Test()]
		public void InsertQuarterSoldOutTest()
		{
			var stateMachine = new GumBallMachineState.GumballMachine(0, 5, _errorHandler, _actionsLogger);
			var nativeMachine = new NaiveGumballMachine.GumballMachine(0, 5, _errorHandler, _actionsLogger);

			void check(IGumballMachine machine)
			{
				Assert.That(() => machine.InsertQuarter(), Throws.Nothing);

				_actionsLogger.DidNotReceive().Log(Arg.Any<string>());
				_errorHandler.Received(1).InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();
			}

			DoActiionWithMachine(stateMachine, check);
			DoActiionWithMachine(nativeMachine, check);
		}

		[Test()]
		public void InsertQuarterHasQuarterTest()
		{
			var stateMachine = new GumBallMachineState.GumballMachine(1, 5, _errorHandler, _actionsLogger);
			var nativeMachine = new NaiveGumballMachine.GumballMachine(1, 5, _errorHandler, _actionsLogger);

			void check(IGumballMachine machine)
			{
				Assert.That(() => machine.InsertQuarter(), Throws.Nothing);
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();

				Assert.That(() => machine.InsertQuarter(), Throws.Nothing);

				_actionsLogger.Received(1).Log(Arg.Any<string>());
				_errorHandler.DidNotReceive().InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();
			}

			DoActiionWithMachine(stateMachine, check);
			DoActiionWithMachine(nativeMachine, check);
		}

		[Test()]
		public void InsertQuarterHasQuarterLimitReachedTest()
		{
			var stateMachine = new GumBallMachineState.GumballMachine(1, 1, _errorHandler, _actionsLogger);
			var nativeMachine = new NaiveGumballMachine.GumballMachine(1, 1, _errorHandler, _actionsLogger);

			void check(IGumballMachine machine)
			{
				Assert.That(() => machine.InsertQuarter(), Throws.Nothing);
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();

				Assert.That(() => machine.InsertQuarter(), Throws.Nothing);

				_actionsLogger.DidNotReceive().Log(Arg.Any<string>());
				_errorHandler.Received(1).InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();
			}

			DoActiionWithMachine(stateMachine, check);
			DoActiionWithMachine(nativeMachine, check);
		}
		#endregion

		#region ejectquarter
		[Test()]
		public void EjectQuarterSoldOutTest()
		{
			var stateMachine = new GumBallMachineState.GumballMachine(0, 5, _errorHandler, _actionsLogger);
			var nativeMachine = new NaiveGumballMachine.GumballMachine(0, 5, _errorHandler, _actionsLogger);

			void check(IGumballMachine machine)
			{
				Assert.That(() => machine.EjectQuarter(), Throws.Nothing);

				_actionsLogger.DidNotReceive().Log(Arg.Any<string>());
				_errorHandler.Received(1).InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();
			}

			DoActiionWithMachine(stateMachine, check);
			DoActiionWithMachine(nativeMachine, check);
		}

		[Test()]
		public void EjectQuarterNoQuarterTest()
		{
			var stateMachine = new GumBallMachineState.GumballMachine(1, 5, _errorHandler, _actionsLogger);
			var nativeMachine = new NaiveGumballMachine.GumballMachine(1, 5, _errorHandler, _actionsLogger);

			void check(IGumballMachine machine)
			{
				Assert.That(() => machine.EjectQuarter(), Throws.Nothing);

				_actionsLogger.DidNotReceive().Log(Arg.Any<string>());
				_errorHandler.Received(1).InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();
			}

			DoActiionWithMachine(stateMachine, check);
			DoActiionWithMachine(nativeMachine, check);
		}

		[Test()]
		public void EjectQuarterHasQuarterTest()
		{
			var stateMachine = new GumBallMachineState.GumballMachine(1, 5, _errorHandler, _actionsLogger);
			var nativeMachine = new NaiveGumballMachine.GumballMachine(1, 5, _errorHandler, _actionsLogger);

			void check(IGumballMachine machine)
			{
				Assert.That(() => machine.InsertQuarter(), Throws.Nothing);
				Assert.That(() => machine.InsertQuarter(), Throws.Nothing);

				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();

				Assert.That(() => machine.EjectQuarter(), Throws.Nothing);

				_actionsLogger.Received(1).Log(Arg.Any<string>());
				_errorHandler.DidNotReceive().InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();
			}

			DoActiionWithMachine(stateMachine, check);
			DoActiionWithMachine(nativeMachine, check);
		}

		[Test()]
		public void EjectQuarterHasQuarterTwiceTest()
		{
			var stateMachine = new GumBallMachineState.GumballMachine(1, 5, _errorHandler, _actionsLogger);
			var nativeMachine = new NaiveGumballMachine.GumballMachine(1, 5, _errorHandler, _actionsLogger);

			void check(IGumballMachine machine)
			{
				Assert.That(() => machine.InsertQuarter(), Throws.Nothing);
				Assert.That(() => machine.InsertQuarter(), Throws.Nothing);
				Assert.That(() => machine.InsertQuarter(), Throws.Nothing);

				Assert.That(() => machine.EjectQuarter(), Throws.Nothing);

				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();

				Assert.That(() => machine.EjectQuarter(), Throws.Nothing);

				_actionsLogger.DidNotReceive().Log(Arg.Any<string>());
				_errorHandler.Received(1).InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();
			}

			DoActiionWithMachine(stateMachine, check);
			DoActiionWithMachine(nativeMachine, check);
		}
		#endregion

		#region turncrank
		[Test()]
		public void TurnCrankSoldOutTest()
		{
			var stateMachine = new GumBallMachineState.GumballMachine(1, 5, _errorHandler, _actionsLogger);
			var nativeMachine = new NaiveGumballMachine.GumballMachine(1, 5, _errorHandler, _actionsLogger);

			void check(IGumballMachine machine)
			{
				Assert.That(() => machine.TurnCrank(), Throws.Nothing);

				_actionsLogger.DidNotReceive().Log(Arg.Any<string>());
				_errorHandler.Received().InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();
			}

			DoActiionWithMachine(stateMachine, check);
			DoActiionWithMachine(nativeMachine, check);
		}

		[Test()]
		public void TurnCrankNoQuarterTest()
		{
			var stateMachine = new GumBallMachineState.GumballMachine(1, 5, _errorHandler, _actionsLogger);
			var nativeMachine = new NaiveGumballMachine.GumballMachine(1, 5, _errorHandler, _actionsLogger);

			void check(IGumballMachine machine)
			{
				Assert.That(() => machine.TurnCrank(), Throws.Nothing);

				_actionsLogger.DidNotReceive().Log(Arg.Any<string>());
				_errorHandler.Received().InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();
			}

			DoActiionWithMachine(stateMachine, check);
			DoActiionWithMachine(nativeMachine, check);
		}

		[Test()]
		public void TurnCrankHasQuarterTest()
		{
			var stateMachine = new GumBallMachineState.GumballMachine(10, 5, _errorHandler, _actionsLogger);
			var nativeMachine = new NaiveGumballMachine.GumballMachine(10, 5, _errorHandler, _actionsLogger);

			void check(IGumballMachine machine)
			{
				Assert.That(() => machine.InsertQuarter(), Throws.Nothing);
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();

				Assert.That(() => machine.TurnCrank(), Throws.Nothing);

				_actionsLogger.Received().Log(Arg.Any<string>());
				_errorHandler.DidNotReceive().InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();
			}

			DoActiionWithMachine(stateMachine, check);
			DoActiionWithMachine(nativeMachine, check);
		}
		#endregion

		#region testcasese
		[Test()]
		public void InsertQuarterTwiceTurnCrankEject()
		{
			var stateMachine = new GumBallMachineState.GumballMachine(10, 5, _errorHandler, _actionsLogger);
			var nativeMachine = new NaiveGumballMachine.GumballMachine(10, 5, _errorHandler, _actionsLogger);

			void check(IGumballMachine machine)
			{
				Assert.That(() => machine.InsertQuarter(), Throws.Nothing);
				_actionsLogger.Received(1).Log(Arg.Any<string>());
				_errorHandler.DidNotReceive().InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();

				Assert.That(() => machine.InsertQuarter(), Throws.Nothing);
				_actionsLogger.Received(1).Log(Arg.Any<string>());
				_errorHandler.DidNotReceive().InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();

				Assert.That(() => machine.TurnCrank(), Throws.Nothing);
				_actionsLogger.Received().Log(Arg.Any<string>());
				_errorHandler.DidNotReceive().InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();

				Assert.That(() => machine.EjectQuarter(), Throws.Nothing);
				_actionsLogger.Received(1).Log(Arg.Any<string>());
				_errorHandler.DidNotReceive().InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();
			}

			DoActiionWithMachine(stateMachine, check);
			DoActiionWithMachine(nativeMachine, check);
		}

		[Test]
		public void InsertQarterTurnCrankEject()
		{
			var stateMachine = new GumBallMachineState.GumballMachine(10, 5, _errorHandler, _actionsLogger);
			var nativeMachine = new NaiveGumballMachine.GumballMachine(10, 5, _errorHandler, _actionsLogger);

			void check(IGumballMachine machine)
			{
				Assert.That(() => machine.InsertQuarter(), Throws.Nothing);
				_actionsLogger.Received(1).Log(Arg.Any<string>());
				_errorHandler.DidNotReceive().InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();

				Assert.That(() => machine.TurnCrank(), Throws.Nothing);
				_actionsLogger.Received().Log(Arg.Any<string>());
				_errorHandler.DidNotReceive().InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();

				Assert.That(() => machine.EjectQuarter(), Throws.Nothing);
				_actionsLogger.DidNotReceive().Log(Arg.Any<string>());
				_errorHandler.Received().InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();
			}

			DoActiionWithMachine(stateMachine, check);
			DoActiionWithMachine(nativeMachine, check);
		}

		[Test]
		public void InsertQuarterTwiceTurnCrankTwiceRefillTurnCrank()
		{
			var stateMachine = new GumBallMachineState.GumballMachine(1, 5, _errorHandler, _actionsLogger);
			var nativeMachine = new NaiveGumballMachine.GumballMachine(1, 5, _errorHandler, _actionsLogger);

			void check(IGumballMachineMaintenance maintenance, IGumballMachine machine)
			{
				Assert.That(() => machine.InsertQuarter(), Throws.Nothing);
				_actionsLogger.Received(1).Log(Arg.Any<string>());
				_errorHandler.DidNotReceive().InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();

				Assert.That(() => machine.InsertQuarter(), Throws.Nothing);
				_actionsLogger.Received(1).Log(Arg.Any<string>());
				_errorHandler.DidNotReceive().InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();

				Assert.That(() => machine.TurnCrank(), Throws.Nothing);
				_actionsLogger.Received().Log(Arg.Any<string>());
				_errorHandler.DidNotReceive().InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();

				Assert.That(() => machine.TurnCrank(), Throws.Nothing);
				_actionsLogger.DidNotReceive().Log(Arg.Any<string>());
				_errorHandler.Received().InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();

				Assert.That(() => maintenance.RefillBalls(5), Throws.Nothing);
				_actionsLogger.Received(1).Log(Arg.Any<string>());
				_errorHandler.DidNotReceive().InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();

				Assert.That(() => machine.TurnCrank(), Throws.Nothing);
				_actionsLogger.Received().Log(Arg.Any<string>());
				_errorHandler.DidNotReceive().InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();

				// no quarters
				Assert.That(() => machine.TurnCrank(), Throws.Nothing);
				_actionsLogger.DidNotReceive().Log(Arg.Any<string>());
				_errorHandler.Received().InvalidAction(Arg.Any<string>());
				_actionsLogger.ClearReceivedCalls();
				_errorHandler.ClearReceivedCalls();
			}

			DoActiionWithMachine(stateMachine, stateMachine, check);
			DoActiionWithMachine(nativeMachine, nativeMachine, check);
		}
		#endregion

		private void DoActiionWithMachine(IGumballMachine machine, Action<IGumballMachine> action)
		{
			action(machine);
		}

		private void DoActiionWithMachine(IGumballMachineMaintenance maintenance, IGumballMachine machine, Action<IGumballMachineMaintenance, IGumballMachine> action)
		{
			action(maintenance, machine);
		}

		private void DoActiionWithMachine(IGumballMachineMaintenance machine, Action<IGumballMachineMaintenance> action)
		{
			action(machine);
		}
	}
}