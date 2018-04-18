using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.document
{
	public class DocumentTitleMemento : IMemento
	{
		private readonly IDocument _document;
		private readonly string _oldTitle;

		public DocumentTitleMemento(IDocument document, string oldTitle)
		{
			_document = document ?? throw new ArgumentNullException(nameof(document));
			_oldTitle = oldTitle ?? throw new ArgumentNullException(nameof(oldTitle));
		}

		public void Restore() => _document.Title = _oldTitle;
	}
}
