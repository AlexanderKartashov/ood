using command.storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.document
{
	public interface IDocument
	{
		void InsertItem(IDocumentItem item, int position);
		void DeleteItem(int position);
		IDocumentItem GetItem(int position);
		int ItemsCount { get; }

		string Title { get; set; }
	}
}
