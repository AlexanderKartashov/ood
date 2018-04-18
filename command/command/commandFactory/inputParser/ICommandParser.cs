using command.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.commandFactory
{
	public interface ICommandParser
	{
		string HelpString { get; }
		string CommandName { get; }
		dynamic ParseCommand(string command);
	}
}
