using GumBallMachineCommon;
using System;
using System.IO;

namespace NaiveGumballMachine
{
	public class GumballMachine : IGumballMachine, IGumballMachineMaintenance
	{
		private readonly IErrorHandler _errorHandler;
		private readonly IActionsLogger _actionsLogger;

		private State _state;
		private uint _ballsCount = 0;
		private uint _quartersCount = 0;
		private uint _quartersLimit = 5;

		public GumballMachine(uint balls, uint quartersLimit, IErrorHandler errorHandler, IActionsLogger actionsLogger)
		{
			_errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
			_actionsLogger = actionsLogger ?? throw new ArgumentNullException(nameof(actionsLogger));
			_ballsCount = balls;
			_state = balls > 0 ? State.NoQuarter : State.SoldOut;
			_quartersLimit = quartersLimit;
		}

		#region IGumballMachine
		public void InsertQuarter()
		{
			switch (_state)
			{
			case State.SoldOut:
				_errorHandler.InvalidAction("You can't insert a quarter, the machine is sold out");
				break;
			case State.NoQuarter:
				_actionsLogger.Log("You inserted a quarter");
				++_quartersCount;
				_state = State.HasQuarter;
				break;
			case State.HasQuarter when _quartersLimit == _quartersCount:
				_errorHandler.InvalidAction("You can't insert another quarter");
				break;
			case State.HasQuarter:
				_actionsLogger.Log("Inserted another quarter");
				++_quartersCount;
				break;
			case State.Sold:
				_errorHandler.InvalidAction("Please wait, we're already giving you a gumball");
				break;
			}
		}

		public void EjectQuarter()
		{
			switch (_state)
			{
			case State.HasQuarter:
				_actionsLogger.Log("Quarters returned");
				_quartersCount = 0;
				_state = State.NoQuarter;
				break;
			case State.NoQuarter:
				_errorHandler.InvalidAction("You haven't inserted a quarter");
				break;
			case State.Sold:
				_errorHandler.InvalidAction("Sorry you already turned the crank");
				break;
			case State.SoldOut when _quartersCount == 0:
				_errorHandler.InvalidAction("You can't eject, you haven't inserted a quarter yet");
				break;
			case State.SoldOut:
				_actionsLogger.Log("Quarters ejected");
				_quartersCount = 0;
				break;
			}
		}

		public void TurnCrank()
		{
			switch (_state)
			{
			case State.SoldOut:
				_errorHandler.InvalidAction("You turned but there's no gumballs");
				break;
			case State.NoQuarter:
				_errorHandler.InvalidAction("You turned but there's no quarter");
				break;
			case State.HasQuarter:
				_actionsLogger.Log("You turned...");
				_state = State.Sold;
				Dispense();
				break;
			case State.Sold:
				_errorHandler.InvalidAction("Turning twice doesn't get you another gumball");
				break;
			}
		}
		#endregion

		#region
		public void RefillBalls(uint balls)
		{
			switch (_state)
			{
			case State.Sold:
				_errorHandler.InvalidAction("You can't refill machine now");
				break;
			case State.HasQuarter:
			case State.NoQuarter:
				_actionsLogger.Log($"Machine refilled, balls added {balls}");
				_ballsCount += balls;
				break;
			case State.SoldOut when _quartersCount > 0:
				_actionsLogger.Log($"Machine refilled, balls added {balls}");
				_ballsCount += balls;
					_state = State.HasQuarter;
				break;
			case State.SoldOut:
				_actionsLogger.Log($"Machine refilled, balls added {balls}");
				_ballsCount += balls;
				_state = State.NoQuarter;
				break;
			}
		}
		#endregion

		public override string ToString()
		{
			string state =
				(_state == State.SoldOut) ? "sold out" :
				(_state == State.NoQuarter) ? "waiting for quarter" :
				(_state == State.HasQuarter) ? "waiting for turn of crank"
											   : "delivering a gumball";
			var qSuffix = _quartersCount != 1 ? "s" : "";
			var bSuffix = _ballsCount != 1 ? "s" : "";
			return $"Mighty Gumball, Inc.\nC++ - enabled Standing Gumball Model #2016\nInventory: " +
				$"{_ballsCount} gumball{bSuffix}, {qSuffix} quarters{qSuffix}{state}";
		}

		private void Dispense()
		{
			switch (_state)
			{
			case State.Sold:
				_actionsLogger.Log("A gumball comes rolling out the slot");
				--_ballsCount;
				--_quartersCount;
				if (_ballsCount == 0)
				{
					//_actionsLogger.Log("Oops, out of gumballs");
					_state = State.SoldOut;
				}
				else
				{
					if (_quartersCount == 0)
					{
						_state = State.NoQuarter;
					}
					else
					{
						_state = State.HasQuarter;
					}
				}
				break;
			case State.NoQuarter:
				_errorHandler.InvalidAction("You need to pay first");
				break;
			case State.SoldOut:
			case State.HasQuarter:
				_errorHandler.InvalidAction("No gumball dispensed");
				break;
			}
		}


	}
}
