using System.Collections.Generic;

namespace command.document
{
	public class DocumentItemsCollection : IDocumentItemsCollection
	{
		private readonly IList<DocumentItem> _documentItems = new List<DocumentItem>();

		public int ItemsCount => _documentItems.Count;
		public void DeleteItem(int position) => _documentItems.RemoveAt(position);
		public void InsertItem(DocumentItem item, int position) => _documentItems.Insert(position, item);
		public DocumentItem GetItem(int position) => _documentItems[position];
	}
}
