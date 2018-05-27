using GumBallMachineCommon;
using System;

namespace GumBallMachineState
{
	public class NoQuarterState : StateBase
	{
		private readonly IGumballMachineFacade _machine;

		public NoQuarterState(IGumballMachineFacade machine, IErrorHandler errorHandler, IActionsLogger logger)
			: base(errorHandler, logger) => _machine = machine ?? throw new ArgumentNullException(nameof(machine));

		public override string DispenseMessage => "You need to pay first";
		public override string EjectQuarterMessage => "You haven't inserted a quarter";
		public override string InsertQuarterMessage => "You inserted a quarter";
		public override string RefillMessage => "Refill machine";
		public override string TurnCrankMessage => "You turned but there's no quarter";
		public override string StateDescription => "waiting for quarter";

		protected override bool BeforeTurnCrank() => false;
		protected override bool BeforeEjectQuarter() => false;
		protected override bool BeforeDispense() => false;

		protected override void InsertQuarterImpl()
		{
			Log(InsertQuarterMessage);
			_machine.SetQuarterInsertedState();
		}

		protected override void RefillImpl(uint balls)
		{
			Log(RefillMessage);
			_machine.Refill(balls);
		}
	}
}
