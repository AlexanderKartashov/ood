using GumBallMachineCommon;
using System;

namespace GumBallMachineState
{
	public abstract class StateBase : IState, IStateMessages
	{
		private readonly IErrorHandler _errorHandler;
		private readonly IActionsLogger _logger;

		public StateBase(IErrorHandler errorHandler, IActionsLogger logger)
		{
			_errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public override sealed string ToString() => StateDescription;

		#region IState
		public void Dispense() => PerformStateActionIfConditionFulfilled(BeforeDispense, DispenseImpl, () => StateCondionsNotFullfiled(DispenseMessage));
		public void EjectQuarter() => PerformStateActionIfConditionFulfilled(BeforeEjectQuarter, EjectQuarterImpl, () => StateCondionsNotFullfiled(EjectQuarterMessage));
		public void InsertQuarter() => PerformStateActionIfConditionFulfilled(BeforeInsertQuarter, InsertQuarterImpl, () => StateCondionsNotFullfiled(InsertQuarterMessage));
		public void Refill(uint balls) => PerformStateActionIfConditionFulfilled(BeforeRefil, () => RefillImpl(balls), () => StateCondionsNotFullfiled(RefillMessage));
		public void TurnCrank() => PerformStateActionIfConditionFulfilled(BeforeTurnCrank, TurnCrankImpl, () => StateCondionsNotFullfiled(TurnCrankMessage));

		public abstract String StateDescription { get; }
		#endregion IState

		#region IStateErrorMessages
		public abstract String DispenseMessage { get; }
		public abstract String EjectQuarterMessage { get; }
		public abstract String InsertQuarterMessage { get; }
		public abstract String RefillMessage { get; }
		public abstract String TurnCrankMessage { get; }
		#endregion IStateErrorMessages

		#region virtual
		protected virtual void DispenseImpl() => throw new InvalidOperationException();
		protected virtual void EjectQuarterImpl() => throw new InvalidOperationException();
		protected virtual void InsertQuarterImpl() => throw new InvalidOperationException();
		protected virtual void RefillImpl(uint balls) => throw new InvalidOperationException();
		protected virtual void TurnCrankImpl() => throw new InvalidOperationException();

		protected virtual bool BeforeTurnCrank() => true;
		protected virtual bool BeforeRefil() => true;
		protected virtual bool BeforeInsertQuarter() => true;
		protected virtual bool BeforeEjectQuarter() => true;
		protected virtual bool BeforeDispense() => true;
		#endregion virtual

		#region nonvirtual
		private void PerformStateActionIfConditionFulfilled(Func<bool> condition, Action onFullfilled, Action onRejected)
		{
			if (condition())
			{
				onFullfilled();
			}
			else
			{
				onRejected();
			}
		}

		protected void Log(string message) => _logger.Log(message);

		private void StateCondionsNotFullfiled(string message) => _errorHandler.InvalidAction(message);
		#endregion nonvirtual
	}
}
