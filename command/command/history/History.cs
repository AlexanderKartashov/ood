using command.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.history
{
	public class History : IHistory
	{
		private readonly IList<AbstractCommand> _commands = new List<AbstractCommand>();
		private readonly uint _maxCommandsCount = uint.MaxValue;
		private  int _executedCommandIndex = -1;

		public History(uint maxCommandsCount = uint.MaxValue)
		{
			_maxCommandsCount = maxCommandsCount;
		}

		public bool CanUndo
		{
			get
			{
				return _commands.Count != 0 && _executedCommandIndex > -1;
			}
		}

		public bool CanRedo
		{
			get
			{
				return _commands.Count != 0 && _executedCommandIndex < _commands.Count - 1;
			}
		}

		/// <summary>
		/// Adds the command.
		/// Command must be already executed
		/// </summary>
		/// <param name="command">The command.</param>
		public void AddCommand(AbstractCommand command)
		{
			if (command == null)
			{
				throw new ArgumentNullException(nameof(command));
			}
			if (!command.IsExecuted)
			{
				throw new ArgumentException(nameof(command));
			}

			RemoveUnexecutedCommands();
			RemoveOldCommands();

			_commands.Add(command);
			_executedCommandIndex = _commands.Count - 1;
		}

		public void Dispose()
		{
			foreach(var command in _commands)
			{
				command.Dispose();
			}
			_commands.Clear();
		}

		public void Redo()
		{
			if (!CanRedo)
			{
				throw new InvalidOperationException("nothing to redo");
			}

			_commands[++_executedCommandIndex].Execute();
		}

		public void Undo()
		{
			if (!CanUndo)
			{
				throw new InvalidOperationException("nothing to undo");
			}

			_commands[_executedCommandIndex--].Unexecute();
		}

		private void RemoveOldCommands()
		{
			if (_commands.Count == _maxCommandsCount)
			{
				_commands[0].Dispose();
				_commands.RemoveAt(0);
			}
		}

		private void RemoveUnexecutedCommands()
		{
			if (CanRedo)
			{
				for (var i = _commands.Count - 1; i > _executedCommandIndex; --i)
				{
					_commands[i].Dispose();
					_commands.RemoveAt(i);
				}
			}
		}
	}
}
