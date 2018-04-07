using painter;
using painter_declarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter_clinet_common
{
	public class PictureDraw
	{
		public static void DrawPictureOnCanvas(ICanvasFactory canvasFactory, CommandLineOptionsProcessor.Options options, IEnumerable<IShapeParser> supportedShapes)
		{
			using (var canvas = canvasFactory.CreateCanvas(options.output, options.w, options.h))
			{
				var designer = new Designer(new ShapeFactory(supportedShapes));
				var draft = designer.CreateDraft(options.reader, Console.Out);
				var painter = new Painter();
				painter.DrawPicture(draft, canvas);
			}
		}
	}
}
