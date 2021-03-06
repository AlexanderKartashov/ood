﻿namespace GumBallMachineState
{
	internal interface IGumballMachineOperations
	{
		void ReleaseBallAndWriteOffQuarter();
		void InsertAdditionalQuarter();
		void EjectAllQuarters();
		void Refill(uint balls);
	}
}