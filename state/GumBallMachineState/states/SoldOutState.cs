using GumBallMachineCommon;
using System;

namespace GumBallMachineState
{
	public class SoldOutState : StateBase
	{
		private readonly IGumballMachineFacade _machine;

		public SoldOutState(IGumballMachineFacade machine, IErrorHandler errorHandler, IActionsLogger logger)
			: base(errorHandler, logger) => _machine = machine ?? throw new ArgumentNullException(nameof(machine));

		public override string DispenseMessage => "No gumball dispensed";
		public override string EjectQuarterMessage => "Quarters returned";
		public override string InsertQuarterMessage => "You can't insert a quarter, the machine is sold out";
		public override string RefillMessage => "Refill machine";
		public override string TurnCrankMessage => "You turned but there's no gumballs";
		public override string StateDescription => "sold out";

		protected override bool BeforeTurnCrank() => false;
		protected override bool BeforeInsertQuarter() => false;
		protected override bool BeforeEjectQuarter() => !_machine.NoMoreQuarters();
		protected override bool BeforeDispense() => false;

		protected override void RefillImpl(uint balls)
		{
			Log(RefillMessage);
			_machine.Refill(balls);
			if (_machine.NoMoreQuarters())
			{
				_machine.SetNoQuartersState();
			}
			else
			{
				_machine.SetQuarterInsertedState();
			}
		}

		protected override void EjectQuarterImpl()
		{
			Log(EjectQuarterMessage);
			_machine.EjectAllQuarters(); // do not change current state after quarters ejection
		}
	}
}
