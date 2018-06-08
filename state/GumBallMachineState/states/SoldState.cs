using GumBallMachineCommon;
using System;

namespace GumBallMachineState
{
	internal class SoldState : StateBase
	{
		private readonly IGumballMachineFacade _machine;

		public SoldState(IGumballMachineFacade machine, IErrorHandler errorHandler, IActionsLogger logger)
			: base(errorHandler, logger, new SoldStateFailedMessages(), new SoldStateSucceededMessages())
		{
			_machine = machine ?? throw new ArgumentNullException(nameof(machine));
		}

		public override string StateDescription => "delivering a gumball";

		protected override bool BeforeRefil() => false;
		protected override bool BeforeInsertQuarter() => false;
		protected override bool BeforeEjectQuarter() => false;
		protected override bool BeforeTurnCrank() => false;

		protected override void DispenseImpl()
		{
			_machine.ReleaseBallAndWriteOffQuarter();
			if (_machine.NoMoreQuarters() && _machine.BallsSoldOut())
			{
				_machine.SetSoldOutState();
			}
			else if (_machine.NoMoreQuarters())
			{
				_machine.SetNoQuartersState();
			}
			else
			{
				_machine.SetQuarterInsertedState();
			}
		}
	}

	public class SoldStateSucceededMessages : IStateMessages
	{
		public string DispenseMessage => "A gumball comes rolling out the slot...";
		public string EjectQuarterMessage => "";
		public string InsertQuarterMessage => "";
		public string RefillMessage => "";
		public string TurnCrankMessage => "";
	}

	public class SoldStateFailedMessages : IStateMessages
	{
		public string DispenseMessage => "";
		public string EjectQuarterMessage => "Please wait, we're already giving you a gumball";
		public string InsertQuarterMessage => "You can't insert a quarter while delivering a gumball";
		public string RefillMessage => "You can't refill machine while delivering a gumball";
		public string TurnCrankMessage => "Turning twice doesn't get you another gumball";
	}
}
