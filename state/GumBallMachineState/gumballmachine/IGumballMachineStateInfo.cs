namespace GumBallMachineState
{
	internal interface IGumballMachineStateInfo
	{
		uint BallsCount { get; }
		uint QuartersCount { get; }
		uint QuartersLimit { get; }
	}
}