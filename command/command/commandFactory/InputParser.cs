using command.document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace command.commandFactory
{
	public class InputParser : IHelpInfo
	{
		private readonly IDocument _document;
		private readonly IDocumentItemFactory _documentItemFactory;
		private readonly IList<ICommandParser> _parsers;

		public InputParser(IDocument document, IDocumentItemFactory documentItemFactory)
		{
			_document = document ?? throw new ArgumentNullException(nameof(document));
			_documentItemFactory = documentItemFactory ?? throw new ArgumentNullException(nameof(documentItemFactory));
			_parsers = new List<ICommandParser>(){
				new DeleteParser(_document),
				new HelpParser(),
				new InsertImageParser(_document, _documentItemFactory),
				new InsertParagraphParser(_document, _documentItemFactory),
				new ListParser(_document),
				new RedoParser(),
				new ReplaceTextParser(_document),
				new ResizeImageParser(_document),
				new SaveParser(_document),
				new SetTitleParser(_document),
				new UndoParser()
			};
		}

		public string HelpText
		{
			get
			{
				StringBuilder builder = new StringBuilder();
				_parsers.ToList().ForEach(x => builder.AppendLine(x.HelpString));
				return builder.ToString();
			}
		}

		public dynamic ParseInput(string command)
		{
			string pattern = @"^(\w+)\s?(.*)$";
			var matches = Regex.Match(command, pattern, RegexOptions.IgnoreCase);
			if (matches.Success && matches.Groups.Count == 3)
			{
				var name = matches.Groups[1].Value;
				var param = matches.Groups[2].Value;
				foreach (var parser in _parsers)
				{
					if (parser.CommandName.ToLower() == name.ToLower())
					{
						return parser.ParseCommand(param);
					}
				}
			}
			return null;
		}
	}
}
