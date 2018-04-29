using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.document
{
	public interface IDocumentItemsCollection
	{
		void InsertItem(IDocumentItem item, int position);
		void DeleteItem(int position);
		int ItemsCount { get; }
		IDocumentItem GetItem(int position);
	}
}
