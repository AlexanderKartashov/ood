using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.commands
{
	public class FunctionalCommand : AbstractCommand
	{
		private readonly Action _onExecute;
		private readonly Action _onUnexecute;
		private readonly Action _onDispose;

		public FunctionalCommand(Action onExecute, Action onUnexecute, Action onDispose)
		{
			_onExecute = onExecute ?? throw new ArgumentNullException(nameof(onExecute));
			_onUnexecute = onUnexecute ?? throw new ArgumentNullException(nameof(onUnexecute));
			_onDispose = onDispose ?? throw new ArgumentNullException(nameof(onDispose));
		}

		protected override void ExecuteImpl()
		{
			_onExecute();
		}

		protected override void UnexecuteImpl()
		{
			_onUnexecute();
		}

		protected override void DisposeImpl()
		{
			_onDispose();
		}
	}
}
