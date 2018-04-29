using command.document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.commands
{
	public class DeleteItem : AbstractCommand
	{
		private readonly IDocument _document;
		private readonly IDocumentItem _item;
		private readonly int _position;

		public DeleteItem(IDocument document, int position)
		{
			_document = document ?? throw new ArgumentNullException(nameof(document));
			if (position > _document.ItemsCount)
			{
				throw new CommandError($"Invalid position {position}");
			}
			_item = _document.GetItem(position);
			_position = position;
		}

		protected override void ExecuteImpl() => _document.DeleteItem(_position);
		protected override void UnexecuteImpl() => _document.InsertItem(_item, _position);

		protected override void DisposeImpl()
		{
			if (IsExecuted)
			{
				_item.Dispose();
			}
		}
	}
}
