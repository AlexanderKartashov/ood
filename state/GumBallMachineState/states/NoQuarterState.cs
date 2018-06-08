using GumBallMachineCommon;
using System;

namespace GumBallMachineState
{
	internal class NoQuarterState : StateBase
	{
		private readonly IGumballMachineFacade _machine;

		public NoQuarterState(IGumballMachineFacade machine, IErrorHandler errorHandler, IActionsLogger logger)
			: base(errorHandler, logger, new NoQuarterStateFailedMessages(), new NoQuarterStateSucceededMessages())
		{
			_machine = machine ?? throw new ArgumentNullException(nameof(machine));
		}

		public override string StateDescription => "waiting for quarter";

		protected override bool BeforeTurnCrank() => false;
		protected override bool BeforeEjectQuarter() => false;
		protected override bool BeforeDispense() => false;
		protected override void InsertQuarterImpl()
		{
			_machine.InsertAdditionalQuarter();
			_machine.SetQuarterInsertedState();
		}
		protected override void RefillImpl(uint balls) => _machine.Refill(balls);
	}

	public class NoQuarterStateSucceededMessages : IStateMessages
	{
		public string DispenseMessage => "";
		public string EjectQuarterMessage => "";
		public string InsertQuarterMessage => "You inserted a quarter";
		public string RefillMessage => "Refill machine";
		public string TurnCrankMessage => "";
	}

	public class NoQuarterStateFailedMessages : IStateMessages
	{
		public string DispenseMessage => "You need to pay first";
		public string EjectQuarterMessage => "You haven't inserted a quarter";
		public string InsertQuarterMessage => "";
		public string RefillMessage => "";
		public string TurnCrankMessage => "You turned but there's no quarter";
	}
}
