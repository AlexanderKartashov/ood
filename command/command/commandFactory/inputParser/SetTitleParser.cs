using command.commands;
using command.document;
using System;
using System.Text.RegularExpressions;

namespace command.commandFactory
{
	public class SetTitleParser : ICommandParser
	{
		private readonly IDocument _document;

		public SetTitleParser(IDocument document) => _document = document ?? throw new ArgumentNullException(nameof(document));

		public string HelpString => "SetTitle <заголовок документа>";
		public string CommandName => "SetTitle";

		public dynamic ParseCommand(string command)
		{
			const string pattern = @"^(.+?)$";
			var matches = Regex.Match(command, pattern, RegexOptions.IgnoreCase);
			if (matches.Success && matches.Groups.Count == 2)
			{
				var title = matches.Groups[1].Value;
				string oldTitle = (string)_document.Title.Clone();
				return new CommandContainer() {
					Command = new FunctionalCommand(
						() => _document.Title = title,
						() => _document.Title = oldTitle,
						() => { }
					)
				};
			}
			throw new ArgumentException($"Invalid command");
		}
	}
}
