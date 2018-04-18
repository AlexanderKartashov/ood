using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.document
{
	public class ParagraphMemento : IMemento
	{
		private readonly IParagraph _paragraph;
		private readonly string _text;

		public ParagraphMemento(IParagraph paragraph, string text)
		{
			_paragraph = paragraph ?? throw new ArgumentNullException(nameof(paragraph));
			_text = text ?? throw new ArgumentNullException(nameof(text));
		}

		public void Restore() => _paragraph.Text = _text;
	}
}
