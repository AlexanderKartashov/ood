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
			using (var canvas = new DisposableCanvasDecorator(canvasFactory.CreateCanvas(options.w, options.h), options.output))
			{
				var designer = new Designer(new ShapeFactory(supportedShapes));
				var draft = designer.CreateDraft(options.reader, options.errorHandler);
				var painter = new Painter();
				painter.DrawPicture(draft, canvas);
			}
		}
	}
}
