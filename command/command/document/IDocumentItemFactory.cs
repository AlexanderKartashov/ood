using command.storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.document
{
	public interface IDocumentItemFactory
	{
		IImage CreateImage(string path, uint width, uint height);
		IParagraph CreateParagraph(string text);
	}
}
