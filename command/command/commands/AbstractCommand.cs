using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.commands
{
	public abstract class AbstractCommand : IDisposable
	{
		public void Execute()
		{
			ExecuteImpl();
			IsExecuted = true;
		}

		public void Unexecute()
		{
			UnexecuteImpl();
			IsExecuted = false;
		}

		public void Dispose() => Destroy();

		protected abstract void ExecuteImpl();
		protected abstract void UnexecuteImpl();
		protected virtual void Destroy() { }

		public virtual bool? IsExecuted { get; protected set; } = null;
	}
}
