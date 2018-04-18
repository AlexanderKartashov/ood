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
		private readonly DocumentItem _item;
		private readonly int _position;

		public DeleteItem(IDocument document, int position)
		{
			_document = document ?? throw new ArgumentNullException(nameof(document));
			_item = _document.GetItem(position);
			_position = position;
		}

		protected override void ExecuteImpl() => _document.DeleteItem(_position);
		protected override void UnexecuteImpl() => _document.InsertItem(_item, _position);

		protected override void Destroy()
		{
			if (IsExecuted.HasValue && IsExecuted.Value)
			{
				_item.Dispose();
			}
		}
	}
}
