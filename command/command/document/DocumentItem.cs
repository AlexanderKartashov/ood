using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.document
{
	public interface DocumentItem : IDisposable
	{
		IParagraph DocumentParagraph { get; }
		IImage DocumentImage { get; }
	}
}
