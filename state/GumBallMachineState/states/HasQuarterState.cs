using GumBallMachineCommon;
using System;

namespace GumBallMachineState
{
	public class HasQuarterState : StateBase
	{
		private readonly IGumballMachineFacade _machine;

		public HasQuarterState(IGumballMachineFacade machine, IErrorHandler errorHandler, IActionsLogger logger)
			: base(errorHandler, logger, new HasQuarterStateFailedMessages(), new HasQuarterStateSucceededMessages())
		{
			_machine = machine ?? throw new ArgumentNullException(nameof(machine));
		}

		public override sealed string StateDescription => "waiting for turn of crank";

		protected sealed override bool BeforeTurnCrank() => !_machine.BallsSoldOut();
		protected override sealed bool BeforeInsertQuarter() => !_machine.QuartersLimitReached();
		protected override sealed bool BeforeDispense() => false;

		protected override void EjectQuarterImpl() => _machine.SetNoQuartersState();
		protected override void InsertQuarterImpl() => _machine.InsertAdditionalQuarter(); // do not change state
		protected override void RefillImpl(uint balls) => _machine.Refill(balls);
		protected override void TurnCrankImpl() => _machine.SetSoldState();
	}

	public class HasQuarterStateSucceededMessages : IStateMessages
	{
		public string DispenseMessage => "";
		public string EjectQuarterMessage => "Quarters returned";
		public string InsertQuarterMessage => "You inserted another quarter";
		public string RefillMessage => "Refill machine";
		public string TurnCrankMessage => "You turned...";
	}

	public class HasQuarterStateFailedMessages : IStateMessages
	{
		public string DispenseMessage => "No gumball dispensed";
		public string EjectQuarterMessage => "";
		public string InsertQuarterMessage => "Quarters limit reached";
		public string RefillMessage => "";
		public string TurnCrankMessage => "";
	}
}
