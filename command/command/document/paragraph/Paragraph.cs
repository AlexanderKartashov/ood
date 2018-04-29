using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.document
{
	public class Paragraph : IParagraph
	{
		public Paragraph(string text) => Text = text ?? throw new ArgumentNullException(nameof(text));

		public string Text { get; set; }

		public IParagraph DocumentParagraph => this;
		public IImage DocumentImage => null;

		public void ChangeText(string text) => Text = text;
		public void Dispose(){}
	}
}
