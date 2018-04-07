using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter_declarations
{
	public interface ICanvasFactory
	{
		ICanvas CreateCanvas(string directoryToSave, int w, int h);
	}
}
