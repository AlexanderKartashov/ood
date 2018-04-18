using command.storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.document
{
	public interface IDocument : IMementoOriginator
	{
		void InsertItem(DocumentItem item, int position);
		int ItemsCount { get; }
		void DeleteItem(int position);
		DocumentItem GetItem(int position);
		string Title { get; set; }
	}
}
