using painter.sdk;
using System.Linq;

namespace painter
{
	public class Painter : IPainter
	{
		public void DrawPicture(PictureDraft draft, ICanvas canvas)
		{
			var shapes = draft.Shapes;
			shapes.ToList().ForEach(x => x.Draw(canvas));
		}
	}
}
