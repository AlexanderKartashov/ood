using painter;
using painter.sdk;
using painter_clinet_common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter_clinet.utils
{
	public class DrawPicture
	{
		private readonly IEnumerable<IShapeParser> _parsers;
		private readonly IEnumerable<ICanvasFactory> _canvasFactories;
		private readonly IEnumerable<ICanvasVisitor> _canvasVisitors;

		public DrawPicture(
			IEnumerable<IShapeParser> parsers,
			IEnumerable<ICanvasFactory> canvaFactories,
			IEnumerable<ICanvasVisitor> canvasVisitors
		)
		{
			_parsers = parsers ?? throw new ArgumentNullException(nameof(parsers));
			_canvasFactories = canvaFactories ?? throw new ArgumentNullException(nameof(canvaFactories));
			_canvasVisitors = canvasVisitors ?? throw new ArgumentNullException(nameof(canvasVisitors));
		}

		public void Draw(CommandLineOptionsProcessor.Options settings)
		{
			settings.ErrorHandler.ShapeInfos = _parsers;

			var designer = new Designer(new ShapeFactory(_parsers));
			var canvases = from factory in _canvasFactories select factory.CreateCanvas(settings.W, settings.H);
			var draft = designer.CreateDraft(settings.Reader, settings.ErrorHandler);
			var painter = new Painter();
			canvases.ToList().ForEach(c => {
				painter.DrawPicture(draft, c);
				_canvasVisitors.ToList().ForEach(v => c.Accept(v));
			});
		}
	}
}
