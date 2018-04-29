using painter.sdk;
using System.ComponentModel.Composition;

namespace canvas
{
	[Export(typeof(ICanvasFactory))]
	public class DummyCanvasFactory : ICanvasFactory
	{
		public ICanvas CreateCanvas(int w, int h)
		{
			return new DummyCanvas(w, h);
		}
	}
}
