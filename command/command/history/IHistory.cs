using command.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.history
{
	public interface IHistory : IDisposable
	{
		void AddCommand(AbstractCommand command);
		bool CanUndo { get; }
		bool CanRedo { get; }
		void Undo();
		void Redo();
	}
}
