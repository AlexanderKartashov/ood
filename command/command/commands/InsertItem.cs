using command.document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.commands
{
	public class InsertItem : AbstractCommand
	{
		private readonly IDocument _document;
		private readonly IDocumentItem _item;
		private readonly int _position;

		public InsertItem(IDocument document, IDocumentItem item, int position)
		{
			_document = document ?? throw new ArgumentNullException(nameof(document));
			_item = item ?? throw new ArgumentNullException(nameof(item));
			if (position > _document.ItemsCount)
			{
				throw new CommandError($"Invalid position {position}");
			}
			_position = position;
		}

		protected override void ExecuteImpl() => _document.InsertItem(_item, _position);
		protected override void UnexecuteImpl() => _document.DeleteItem(_position);

		protected override void DisposeImpl()
		{
			if (!IsExecuted)
			{
				_item.Dispose();
			}
		}
	}
}
