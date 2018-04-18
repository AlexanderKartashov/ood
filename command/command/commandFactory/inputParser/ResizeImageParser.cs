using command.commands;
using command.document;
using System;
using System.Text.RegularExpressions;

namespace command.commandFactory
{
	public class ResizeImageParser : ICommandParser
	{
		private readonly IDocument _document;

		public ResizeImageParser(IDocument document) => _document = document ?? throw new ArgumentNullException(nameof(document));

		public string HelpString => "ResizeImage <позиция> <ширина> <высота>";
		public string CommandName => "ResizeImage";

		public dynamic ParseCommand(string command)
		{
			const string pattern = @"^(\d+?)\s(\d+?)\s(\d+?)$";
			var matches = Regex.Match(command, pattern, RegexOptions.IgnoreCase);
			if (matches.Success && matches.Groups.Count == 4)
			{
				var pos = matches.Groups[1].Value;
				var width = matches.Groups[2].Value;
				var height = matches.Groups[3].Value;
				var position = int.Parse(pos);
				return new CommandContainer() { Command = new ResizeImage(_document.GetItem(position).DocumentImage, uint.Parse(width), uint.Parse(height)) };
			}
			throw new ArgumentException($"Invalid command");
		}
	}
}
