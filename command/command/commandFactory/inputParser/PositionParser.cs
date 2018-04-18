using command.document;
using System;

namespace command.commandFactory
{
	public class PositionParser
	{
		private readonly IDocument _document;

		public PositionParser(IDocument document) => _document = document ?? throw new ArgumentNullException(nameof(document));

		public int ParsePosition(string pos)
		{
			if (pos == "end")
			{
				return _document.ItemsCount;
			}
			else
			{
				return int.Parse(pos);
			}
		}
	}
}
