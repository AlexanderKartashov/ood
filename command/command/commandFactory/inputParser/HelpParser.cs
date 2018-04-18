using command.commands;
using System;
using System.Text.RegularExpressions;

namespace command.commandFactory
{
	public class HelpParser : ICommandParser
	{
		public string HelpString => CommandName;
		public string CommandName => "Help";

		public dynamic ParseCommand(string command)
		{
			return new ActionContainer() { SelectedAction = ActionContainer.Action.Help };
		}
	}
}
