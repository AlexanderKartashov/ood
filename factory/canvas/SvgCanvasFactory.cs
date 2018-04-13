using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using painter_declarations;
using System.ComponentModel.Composition;

namespace canvas
{
	[Export(typeof(ICanvasFactory))]
	public class SvgCanvasFactory : ICanvasFactory
	{
		public ICanvas CreateCanvas(int w, int h)
		{
			return new SvgCanvas(w, h);
		}
	}
}
