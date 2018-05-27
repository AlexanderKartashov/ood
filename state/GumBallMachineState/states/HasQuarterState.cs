using GumBallMachineCommon;
using System;

namespace GumBallMachineState
{
	public class HasQuarterState : StateBase
	{
		private readonly IGumballMachineFacade _machine;

		public HasQuarterState(IGumballMachineFacade machine, IErrorHandler errorHandler, IActionsLogger logger)
			: base(errorHandler, logger) => _machine = machine ?? throw new ArgumentNullException(nameof(machine));

		public override sealed string StateDescription => "waiting for turn of crank";
		public override sealed string DispenseMessage => "No gumball dispensed";
		public override sealed string EjectQuarterMessage => "Quarters returned";
		public override sealed string InsertQuarterMessage => "You inserted another quarter";
		public override sealed string RefillMessage => "Refill machine";
		public override sealed string TurnCrankMessage => "You turned...";

		protected sealed override bool BeforeTurnCrank() => _machine.BallsSoldOut();
		protected override sealed bool BeforeInsertQuarter() => _machine.QuartersLimitReached();
		protected override sealed bool BeforeDispense() => false;

		protected override void EjectQuarterImpl()
		{
			Log(EjectQuarterMessage);
			_machine.SetNoQuartersState();
		}

		protected override void InsertQuarterImpl()
		{
			Log(InsertQuarterMessage);
			_machine.InsertAdditionalQuarter(); // do not change state
		}

		protected override void RefillImpl(uint balls)
		{
			Log(RefillMessage);
			_machine.Refill(balls);
		}

		protected override void TurnCrankImpl()
		{
			Log(TurnCrankMessage);
			_machine.SetSoldState();
		}
	}
}
