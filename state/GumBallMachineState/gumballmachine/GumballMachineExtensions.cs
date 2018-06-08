namespace GumBallMachineState
{
	internal static class GumballMachineExtensions
	{
		public static bool QuartersLimitReached(this IGumballMachineStateInfo state)
		{
			return state.QuartersCount == state.QuartersLimit;
		}

		public static bool BallsSoldOut(this IGumballMachineStateInfo state)
		{
			return state.BallsCount == 0;
		}

		public static bool NoMoreQuarters(this IGumballMachineStateInfo state)
		{
			return state.QuartersCount == 0;
		}
	}
}
