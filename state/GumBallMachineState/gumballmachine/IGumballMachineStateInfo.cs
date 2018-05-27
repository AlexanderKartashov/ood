namespace GumBallMachineState
{
	public interface IGumballMachineStateInfo
	{
		uint BallsCount { get; }
		uint QuartersCount { get; }
		uint QuartersLimit { get; }
	}
}