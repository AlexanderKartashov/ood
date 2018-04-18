using command.document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.commands
{
	public class ChangeParagraphText : AbstractCommand
	{
		private readonly IParagraph _paragraph;
		private readonly IMemento _memento;
		private readonly string _newText;

		public ChangeParagraphText(IParagraph paragraph, string newText)
		{
			_paragraph = paragraph ?? throw new ArgumentNullException(nameof(paragraph));
			_newText = newText ?? throw new ArgumentNullException(nameof(newText));
			_memento = _paragraph.CreateMemento();
		}

		protected override void ExecuteImpl() => _paragraph.Text = _newText;
		protected override void UnexecuteImpl() => _memento.Restore();
	}
}
