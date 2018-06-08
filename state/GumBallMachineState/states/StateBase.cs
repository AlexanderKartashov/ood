using GumBallMachineCommon;
using System;

namespace GumBallMachineState
{
	internal abstract class StateBase : IState
	{
		private readonly IErrorHandler _errorHandler;
		private readonly IActionsLogger _logger;
		private readonly IStateMessages _errroMessages;
		private readonly IStateMessages _actionMessages;
		private IErrorHandler errorHandler;
		private IActionsLogger logger;

		public StateBase(IErrorHandler errorHandler, IActionsLogger logger, IStateMessages errroMessages, IStateMessages actionMessages)
		{
			_errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_errroMessages = errroMessages ?? throw new ArgumentNullException(nameof(errroMessages));
			_actionMessages = actionMessages ?? throw new ArgumentNullException(nameof(actionMessages));
		}

		protected StateBase(IErrorHandler errorHandler, IActionsLogger logger)
		{
			this.errorHandler = errorHandler;
			this.logger = logger;
		}

		public override sealed string ToString() => StateDescription;

		#region IState
		public void Dispense() => PerformStateActionIfConditionFulfilled(
			BeforeDispense, DispenseImpl, StateCondionsNotFullfiled, _actionMessages.DispenseMessage, _errroMessages.DispenseMessage);
		public void EjectQuarter() => PerformStateActionIfConditionFulfilled(
			BeforeEjectQuarter, EjectQuarterImpl, StateCondionsNotFullfiled, _actionMessages.EjectQuarterMessage, _errroMessages.EjectQuarterMessage);
		public void InsertQuarter() => PerformStateActionIfConditionFulfilled(
			BeforeInsertQuarter, InsertQuarterImpl, StateCondionsNotFullfiled, _actionMessages.InsertQuarterMessage, _errroMessages.InsertQuarterMessage);
		public void Refill(uint balls) => PerformStateActionIfConditionFulfilled(
			BeforeRefil, () => RefillImpl(balls), StateCondionsNotFullfiled, _actionMessages.RefillMessage, _errroMessages.RefillMessage);
		public void TurnCrank() => PerformStateActionIfConditionFulfilled(
			BeforeTurnCrank, TurnCrankImpl, StateCondionsNotFullfiled, _actionMessages.TurnCrankMessage, _errroMessages.TurnCrankMessage);

		public abstract String StateDescription { get; }

		#endregion IState

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
		private void PerformStateActionIfConditionFulfilled(Func<bool> condition, Action onFullfilled, Action onRejected, String succeededMessage, String errorMessage)
		{
			if (condition())
			{
				LogSucceeded(succeededMessage);
				onFullfilled();
			}
			else
			{
				LogError(errorMessage);
				onRejected();
			}
		}

		protected void LogSucceeded(string message) => _logger.Log(message);
		protected void LogError(string message) => _errorHandler.InvalidAction(message);

		private void StateCondionsNotFullfiled() { }
		#endregion nonvirtual
	}
}
