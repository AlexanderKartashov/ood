using command.document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.commands
{
	public class ChangeDocumentTitle : AbstractCommand
	{
		private readonly IDocument _document;
		private readonly string _newTitle;
		private readonly IMemento _memento;

		public ChangeDocumentTitle(IDocument document, string title)
		{
			_document = document ?? throw new ArgumentNullException(nameof(document));
			_newTitle = title ?? throw new ArgumentNullException(nameof(title));
			_memento = _document.CreateMemento();
		}

		protected override void ExecuteImpl() => _document.Title = _newTitle;
		protected override void UnexecuteImpl() => _memento.Restore();
	}
}
