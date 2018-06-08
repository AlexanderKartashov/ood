namespace GumBallMachineState
{
	internal interface IGumballMachineStatesMachine
	{
		void SetNoQuartersState();
		void SetQuarterInsertedState();
		void SetSoldOutState();
		void SetSoldState();
	}
}