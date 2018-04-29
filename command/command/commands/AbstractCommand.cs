using System;

namespace command.commands
{
	public abstract class AbstractCommand : IDisposable
	{
		private bool _isDisposed = false;

		public void Execute()
		{
			if (IsExecuted)
			{
				throw new InvalidOperationException("attempt to execute already executed command");
			}
			ExecuteImpl();
			IsExecuted = true;
		}

		public void Unexecute()
		{
			if (!IsExecuted)
			{
				throw new InvalidOperationException("attempt to unexecute already unexecuted command");
			}
			UnexecuteImpl();
			IsExecuted = false;
		}

		public void Dispose()
		{
			if (_isDisposed)
			{
				return;
			}

			DisposeImpl();
			_isDisposed = true;
		}

		protected abstract void ExecuteImpl();
		protected abstract void UnexecuteImpl();
		protected virtual void DisposeImpl() {}

		public virtual bool IsExecuted { get; protected set; } = false;
	}
}
