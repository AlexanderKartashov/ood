using command.commands;
using System;
using System.Text.RegularExpressions;

namespace command.commandFactory
{
	public class RedoParser : ICommandParser
	{
		public string CommandName => "Redo";
		public string HelpString => CommandName;

		public dynamic ParseCommand(string command)
		{
			return new ActionContainer() { SelectedAction = ActionContainer.Action.Redo };
		}
	}
}
