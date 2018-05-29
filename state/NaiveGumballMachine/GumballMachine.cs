using System;
using System.IO;

namespace NaiveGumballMachine
{
	public class GumballMachine
	{
		private readonly TextWriter _textWriter;
		private State _state;
		private uint _ballsCount = 0;
		private uint _quartersCount = 0;
		private uint _quartersLimit = 5;

		public GumballMachine(TextWriter textWriter, uint count)
		{
			_textWriter = textWriter ?? throw new ArgumentNullException(nameof(textWriter));
			_ballsCount = count;
			_state = count > 0 ? State.NoQuarter : State.SoldOut;
		}

		public void InsertQuarter()
		{
			switch (_state)
			{
			case State.SoldOut:
				_textWriter.WriteLine("You can't insert a quarter, the machine is sold out");
				break;
			case State.NoQuarter:
				_textWriter.WriteLine("You inserted a quarter");
				++_quartersCount;
				_state = State.HasQuarter;
				break;
			case State.HasQuarter when _quartersLimit == _quartersCount:
				++_quartersCount;
				_textWriter.WriteLine("You can't insert another quarter");
				break;
			case State.HasQuarter:
				_textWriter.WriteLine("Inserted another quarter");
				++_quartersCount;
				break;
			case State.Sold:
				_textWriter.WriteLine("Please wait, we're already giving you a gumball");
				break;
			}
		}

		public void EjectQuarter()
		{
			switch (_state)
			{
			case State.HasQuarter:
				_textWriter.WriteLine("Quarters returned");
				_quartersCount = 0;
				_state = State.NoQuarter;
				break;
			case State.NoQuarter:
				_textWriter.WriteLine("You haven't inserted a quarter");
				break;
			case State.Sold:
				_textWriter.WriteLine("Sorry you already turned the crank");
				break;
			case State.SoldOut when _quartersCount == 0:
				_textWriter.WriteLine("You can't eject, you haven't inserted a quarter yet");
				break;
			case State.SoldOut:
				_textWriter.WriteLine("Quarters ejected");
				break;
			}
		}

		public void TurnCrank()
		{
			switch (_state)
			{
			case State.SoldOut:
				_textWriter.WriteLine("You turned but there's no gumballs");
				break;
			case State.NoQuarter:
				_textWriter.WriteLine("You turned but there's no quarter");
				break;
			case State.HasQuarter:
				_textWriter.WriteLine("You turned...");
				_state = State.Sold;
				Dispense();
				break;
			case State.Sold:
				_textWriter.WriteLine("Turning twice doesn't get you another gumball");
				break;
			}
		}

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
				_textWriter.WriteLine("A gumball comes rolling out the slot");
				--_ballsCount;
				--_quartersCount;
				if (_ballsCount == 0)
				{
					_textWriter.WriteLine("Oops, out of gumballs");
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
				_textWriter.WriteLine("You need to pay first");
				break;
			case State.SoldOut:
			case State.HasQuarter:
				_textWriter.WriteLine("No gumball dispensed");
				break;
			}
		}
	}
}
