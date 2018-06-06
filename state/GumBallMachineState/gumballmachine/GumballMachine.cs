using GumBallMachineCommon;
using System;

namespace GumBallMachineState
{
	public class GumballMachine : IGumballMachineFacade, IGumballMachine, IGumballMachineMaintenance
	{
		private IState _currentState;
		private IActionsLogger _logger;
		private IErrorHandler _errorHandler;
		private IState CurrentState
		{
			get => _currentState ?? throw new InvalidOperationException("current gumball machine not initialized");
			set => _currentState = value;
		}

		public GumballMachine(uint balls, uint quartersLimit, IErrorHandler errorHandler, IActionsLogger logger)
		{
			BallsCount = balls;
			QuartersLimit = quartersLimit != 0 ? quartersLimit : throw new ArgumentException("Quarters limit must be grater than zero");

			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));

			if (BallsCount > 0)
			{
				SetNoQuartersState();
			}
			else
			{
				SetSoldOutState();
			}
		}

		public override string ToString()
		{
			var ballsExt = BallsCount != 1 ? "s" : "";
			var quartersExt = QuartersCount != 1 ? "s" : "";
			var message =
				$"Mighty Gumball, Inc.\nC# - enabled Standing Gumball Model #2018 (with state)\n" +
				$"Inventory: {BallsCount} gumball{ballsExt}, {QuartersCount} quarter{quartersExt}\n" +
				$"Machine is {CurrentState.ToString()}";
			return message;
		}

		#region IGumballMachine
		public void EjectQuarter() => CurrentState.EjectQuarter();
		public void InsertQuarter() => CurrentState.InsertQuarter();
		public void TurnCrank()
		{
			CurrentState.TurnCrank();
			CurrentState.Dispense();
		}
		#endregion IGumballMachine

		#region IGumballMachineMaintenance
		public void RefillBalls(uint balls) => CurrentState.Refill(balls);
		#endregion IGumballMachineMaintenance

		#region IGumballMachineStateInfo
		public uint BallsCount { get; private set; } = 0;
		public uint QuartersCount { get; private set; } = 0;
		public uint QuartersLimit { get; private set; } = 0;
		#endregion IGumballMachineStateInfo

		#region IGumballMachingOperations
		public void Refill(uint count) => BallsCount += count;
		public void ReleaseBallAndWriteOffQuarter()
		{
			--BallsCount;
			--QuartersCount;
		}
		public void InsertAdditionalQuarter() => ++QuartersCount;
		public void EjectAllQuarters() => QuartersCount = 0;
		#endregion

		#region IGumballMachineStatesMachine
		public void SetNoQuartersState()
		{
			EjectAllQuarters();
			SetState(new NoQuarterState(this, _errorHandler, _logger));
		}
		public void SetQuarterInsertedState()
		{
			SetState(new HasQuarterState(this, _errorHandler, _logger));
		}
		public void SetSoldState() => SetState(new SoldState(this, _errorHandler, _logger));
		public void SetSoldOutState() => SetState(new SoldOutState(this, _errorHandler, _logger));
		#endregion IGumballMachineStatesMachine

		private void SetState(IState newState) => CurrentState = newState;
	}
}
