using command.commands;
using command.document;
using System;
using System.Text.RegularExpressions;

namespace command.commandFactory
{
	public class ListParser : ICommandParser
	{
		private readonly IDocument _document;

		public ListParser(IDocument document) => _document = document ?? throw new ArgumentNullException(nameof(document));

		public string HelpString => CommandName;
		public string CommandName => "List";

		public dynamic ParseCommand(string command)
		{
			return new ListAction(){ Document = _document };
		}
	}
}
