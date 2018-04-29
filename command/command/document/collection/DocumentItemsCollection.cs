using System.Collections.Generic;

namespace command.document
{
	public class DocumentItemsCollection : IDocumentItemsCollection
	{
		private readonly IList<IDocumentItem> _documentItems = new List<IDocumentItem>();

		public int ItemsCount => _documentItems.Count;
		public void DeleteItem(int position) => _documentItems.RemoveAt(position);
		public void InsertItem(IDocumentItem item, int position) => _documentItems.Insert(position, item);
		public IDocumentItem GetItem(int position) => _documentItems[position];
	}
}
