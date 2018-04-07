using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter
{
	public interface IDesigner
	{
		PictureDraft CreateDraft(TextReader textReader, TextWriter errorReporter);
	}
}
