using painter.sdk;
using System.ComponentModel.Composition;

namespace canvas
{
	[Export(typeof(ICanvasFactory))]
	public class BitmapCanvasFactory : ICanvasFactory
	{
		public ICanvas CreateCanvas(int w, int h)
		{
			return new BitmapCanvas(w, h);
		}
	}
}
