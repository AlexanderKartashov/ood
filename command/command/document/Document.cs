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
		public int ItemsCount { get => _documentItems.ItemsCount; }

		public void DeleteItem(int position) => _documentItems.DeleteItem(position);
		public IDocumentItem GetItem(int position) => _documentItems.GetItem(position);
		public void InsertItem(IDocumentItem item, int position) => _documentItems.InsertItem(item, position);
	}

}
