using command.storage;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.document
{
	public interface IImage : IDocumentItem
	{
		uint Width { get; set; }
		uint Height { get; set; }
		IResource Resource { get; }
	}
}
