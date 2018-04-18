using command.commands;
using command.document;
using System;
using System.Text.RegularExpressions;

namespace command.commandFactory
{
	public class InsertParagraphParser : ICommandParser
	{
		private readonly IDocument _document;
		private readonly IDocumentItemFactory _documentItemFactory;

		public InsertParagraphParser(IDocument document, IDocumentItemFactory documentItemFactory)
		{
			_document = document ?? throw new ArgumentNullException(nameof(document));
			_documentItemFactory = documentItemFactory ?? throw new ArgumentNullException(nameof(documentItemFactory));
		}

		public string HelpString => "InsertParagraph <позиция>|end <текст параграфа>";
		public string CommandName => "InsertParagraph";

		public dynamic ParseCommand(string command)
		{
			const string pattern = @"^(\d+?|end)\s(.+)$";
			var matches = Regex.Match(command, pattern, RegexOptions.IgnoreCase);
			if (matches.Success && matches.Groups.Count == 3)
			{
				var pos = matches.Groups[1].Value;
				var text = matches.Groups[2].Value;

				var parser = new PositionParser(_document);
				return new CommandContainer() { Command = new InsertItem(_document, _documentItemFactory.CreateParagraph(text), parser.ParsePosition(pos)) };
			}
			throw new ArgumentException($"Invalid command");
		}
	}
}
