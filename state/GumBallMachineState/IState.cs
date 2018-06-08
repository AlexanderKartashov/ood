using System;

namespace GumBallMachineState
{
	internal interface IState
	{
		void InsertQuarter();
		void EjectQuarter();
		void TurnCrank();
		void Dispense();
		void Refill(uint balls);

		String StateDescription { get; }
	}
}
