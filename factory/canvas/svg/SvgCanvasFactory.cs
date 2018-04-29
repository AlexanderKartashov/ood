using System.ComponentModel.Composition;
using painter.sdk;

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
