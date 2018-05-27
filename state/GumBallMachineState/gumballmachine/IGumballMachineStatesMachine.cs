namespace GumBallMachineState
{
	public interface IGumballMachineStatesMachine
	{
		void SetNoQuartersState();
		void SetQuarterInsertedState();
		void SetSoldOutState();
		void SetSoldState();
	}
}