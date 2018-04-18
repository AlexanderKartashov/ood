using command.storage;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.document
{
	public class Document : IDocument
	{
		private readonly DocumentItemsCollection _documentItems = new DocumentItemsCollection();

		public string Title { get; set; } = "New document";
		public IMemento CreateMemento() => new DocumentTitleMemento(this, String.Copy(Title));

		public int ItemsCount { get => _documentItems.ItemsCount; }
		public void DeleteItem(int position) => _documentItems.DeleteItem(position);
		public DocumentItem GetItem(int position) => _documentItems.GetItem(position);
		public void InsertItem(DocumentItem item, int position) => _documentItems.InsertItem(item, position);
	}

}
