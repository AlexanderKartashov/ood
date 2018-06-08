using GumBallMachineCommon;
using System;

namespace GumBallMachineState
{
	internal class SoldOutState : StateBase
	{
		private readonly IGumballMachineFacade _machine;

		public SoldOutState(IGumballMachineFacade machine, IErrorHandler errorHandler, IActionsLogger logger)
			: base(errorHandler, logger, new SoldOutStateFailedMessages(), new SoldOutStateSucceededMessages())
		{
			_machine = machine ?? throw new ArgumentNullException(nameof(machine));
		}

		public override string StateDescription => "sold out";

		protected override bool BeforeTurnCrank() => false;
		protected override bool BeforeInsertQuarter() => false;
		protected override bool BeforeEjectQuarter() => !_machine.NoMoreQuarters();
		protected override bool BeforeDispense() => false;

		protected override void RefillImpl(uint balls)
		{
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

		protected override void EjectQuarterImpl() => _machine.EjectAllQuarters(); // do not change current state after quarters ejection
	}

	public class SoldOutStateSucceededMessages : IStateMessages
	{
		public string DispenseMessage => "";
		public string EjectQuarterMessage => "Quarters returned";
		public string InsertQuarterMessage => "";
		public string RefillMessage => "Refill machine";
		public string TurnCrankMessage => "";
	}

	public class SoldOutStateFailedMessages : IStateMessages
	{
		public string DispenseMessage => "No gumball dispensed";
		public string EjectQuarterMessage => "You haven't inserted a quarter";
		public string InsertQuarterMessage => "You can't insert a quarter, the machine is sold out";
		public string RefillMessage => "";
		public string TurnCrankMessage => "You turned but there's no gumballs";
	}
}
