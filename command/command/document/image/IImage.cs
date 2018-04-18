using command.storage;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.document
{
	public interface IImage : IMementoOriginator, DocumentItem
	{
		uint Width { get; }
		uint Height { get; }
		IResource Resource { get; }

		void Resize(uint imageWidth, uint imageHeight);
	}
}
