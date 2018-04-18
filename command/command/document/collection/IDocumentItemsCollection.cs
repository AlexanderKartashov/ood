using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.document
{
	public interface IDocumentItemsCollection
	{
		void InsertItem(DocumentItem item, int position);
		void DeleteItem(int position);
		int ItemsCount { get; }
		DocumentItem GetItem(int position);
	}
}
