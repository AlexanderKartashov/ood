using command.commands;
using System;
using System.Text.RegularExpressions;

namespace command.commandFactory
{
	public class UndoParser : ICommandParser
	{
		public string HelpString => CommandName;
		public string CommandName => "Undo";

		public dynamic ParseCommand(string command)
		{
			return new ActionContainer() { SelectedAction = ActionContainer.Action.Undo };
		}
	}
}
