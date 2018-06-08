using System;

namespace GumBallMachineState
{
	internal interface IStateMessages
	{
		String DispenseMessage { get; }
		String EjectQuarterMessage { get; }
		String InsertQuarterMessage { get; }
		String RefillMessage { get; }
		String TurnCrankMessage { get; }
	}
}
