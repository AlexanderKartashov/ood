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
				if (position > _document.ItemsCount)
				{
					throw new CommandError($"Invalid position {position}");
				}
				var image = _document.GetItem(position).DocumentImage ?? throw new CommandError("not an image");
				var newW = uint.Parse(width);
				var newH = uint.Parse(height);
				var oldW = image.Width;
				var oldH = image.Height;
				return new CommandContainer() {
					Command = new FunctionalCommand(
						() => { image.Width = newW; image.Height = newH; },
						() => { image.Width = oldW; image.Height = oldH; },
						() => { }
					)
				};
			}
			throw new ArgumentException($"Invalid command");
		}
	}
}
