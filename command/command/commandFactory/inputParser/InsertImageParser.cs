using command.commands;
using command.document;
using System;
using System.Text.RegularExpressions;

namespace command.commandFactory
{
	public class InsertImageParser : ICommandParser
	{
		private readonly IDocument _document;
		private readonly IDocumentItemFactory _documentItemFactory;

		public InsertImageParser(IDocument document, IDocumentItemFactory documentItemFactory)
		{
			_document = document ?? throw new ArgumentNullException(nameof(document));
			_documentItemFactory = documentItemFactory ?? throw new ArgumentNullException(nameof(documentItemFactory));
		}

		public string HelpString => "InsertImage <позиция>|end <ширина> <высота> <путь к файлу изображения>";
		public string CommandName => "InsertImage";

		public dynamic ParseCommand(string command)
		{
			const string pattern = @"^(\d+?|end)\s(\d+?)\s(\d+?)\s(.+)$";
			var matches = Regex.Match(command, pattern, RegexOptions.IgnoreCase);
			if (matches.Success && matches.Groups.Count == 5)
			{
				var pos = matches.Groups[1].Value;
				var width = matches.Groups[2].Value;
				var height = matches.Groups[3].Value;
				var path = matches.Groups[4].Value;

				var parser = new PositionParser(_document);
				return new CommandContainer() { Command = new InsertItem(_document, _documentItemFactory.CreateImage(path, uint.Parse(width), uint.Parse(height)), parser.ParsePosition(pos)) };
			}
			throw new ArgumentException($"Invalid command");
		}
	}
}
