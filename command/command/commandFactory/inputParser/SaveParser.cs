using command.commands;
using command.document;
using System;
using System.Text.RegularExpressions;

namespace command.commandFactory
{
	public class SaveParser : ICommandParser
	{
		private readonly IDocument _document;

		public SaveParser(IDocument document) => _document = document ?? throw new ArgumentNullException(nameof(document));

		public string HelpString => "Save <путь>";
		public string CommandName => "Save";

		public dynamic ParseCommand(string command)
		{
			const string pattern = @"^(.+)$";
			var matches = Regex.Match(command, pattern, RegexOptions.IgnoreCase);
			if (matches.Success && matches.Groups.Count == 2)
			{
				var path = matches.Groups[1].Value;
				return new SaveAction() { PathToSave = path, Document = _document };
			}
			throw new ArgumentException($"Invalid command");
		}
	}
}
