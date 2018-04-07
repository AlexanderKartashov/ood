using painter_declarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
