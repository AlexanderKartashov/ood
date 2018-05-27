using GumBallMachineCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GumBallMachineState
{
	public class SoldState : StateBase
	{
		private readonly IGumballMachineFacade _machine;

		public SoldState(IGumballMachineFacade machine, IErrorHandler errorHandler, IActionsLogger logger)
			: base(errorHandler, logger) => _machine = machine ?? throw new ArgumentNullException(nameof(machine));

		public override string DispenseMessage => "A gumball comes rolling out the slot...";
		public override string EjectQuarterMessage => "Please wait, we're already giving you a gumball";
		public override string InsertQuarterMessage => "You can't insert a quarter while delivering a gumball";
		public override string RefillMessage => "You can't refill machine while delivering a gumball";
		public override string TurnCrankMessage => "Turning twice doesn't get you another gumball";
		public override string StateDescription => "delivering a gumball";

		protected override bool BeforeRefil() => false;
		protected override bool BeforeInsertQuarter() => false;
		protected override bool BeforeEjectQuarter() => false;
		protected override bool BeforeTurnCrank() => false;

		protected override void DispenseImpl()
		{
			Log(StateDescription);
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
				_machine.SetSoldOutState();
			}
		}
	}
}
