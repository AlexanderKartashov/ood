using command.commands;
using command.document;
using System;
using System.Text.RegularExpressions;

namespace command.commandFactory
{
	public class ReplaceTextParser : ICommandParser
	{
		private readonly IDocument _document;

		public ReplaceTextParser(IDocument document) => _document = document ?? throw new ArgumentNullException(nameof(document));

		public string HelpString => "ReplaceText <позиция> <текст параграфа>";
		public string CommandName => "ReplaceText";

		public dynamic ParseCommand(string command)
		{
			const string pattern = @"^(\d+?)\s(.+)$";
			var matches = Regex.Match(command, pattern, RegexOptions.IgnoreCase);
			if (matches.Success && matches.Groups.Count == 3)
			{
				var pos = matches.Groups[1].Value;
				var text = matches.Groups[2].Value;
				var position = int.Parse(pos);
				if (position > _document.ItemsCount)
				{
					throw new CommandError($"Invalid position {position}");
				}
				return new CommandContainer() { Command = new ChangeParagraphText(_document.GetItem(position).DocumentParagraph, text) };
			}
			throw new ArgumentException($"Invalid command");
		}
	}
}
