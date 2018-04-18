using command.commands;
using command.document;
using System;
using System.Text.RegularExpressions;

namespace command.commandFactory
{
	public class DeleteParser : ICommandParser
	{
		private readonly IDocument _document;

		public DeleteParser(IDocument document) => _document = document ?? throw new ArgumentNullException(nameof(document));

		public string HelpString => "DeleteItem <позиция>";
		public string CommandName => "DeleteItem";

		public dynamic ParseCommand(string command)
		{
			const string pattern = @"^(\d+?)$";
			var matches = Regex.Match(command, pattern, RegexOptions.IgnoreCase);
			if (matches.Success && matches.Groups.Count == 2)
			{
				var pos = matches.Groups[1].Value;
				int position = int.Parse(pos);
				return new CommandContainer() { Command = new DeleteItem(_document, position) };
			}
			throw new ArgumentException($"Invalid command");
		}
	}
}
