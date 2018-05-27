using System;

namespace GumBallMachineState
{
	public interface IState
	{
		void InsertQuarter();
		void EjectQuarter();
		void TurnCrank();
		void Dispense();
		void Refill(uint balls);

		String StateDescription { get; }
	}
}
