using canvas;
using CommandLine;
using painter;
using painter.parsers;
using painter.sdk;
using painter_clinet.utils;
using painter_clinet_common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter_client_simple
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				var options = new CommandLineOptions();
				Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed(x => DrawPicture(x));
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		static void DrawPicture(CommandLineOptions options)
		{
			CommandLineOptionsProcessor processor = new CommandLineOptionsProcessor();
			var settings = processor.Process(options);

			var parsers = new List<IShapeParser>() {
				new TriangleParser(),
				new RectangleParser(),
				new RectangularPolygonParser(),
				new EllipseParser()
			};

			var factories = new List<ICanvasFactory>() {
				new SvgCanvasFactory(),
				new BitmapCanvasFactory(),
				new DummyCanvasFactory()
			};

			settings.ErrorHandler.ShapeInfos = parsers;

			var designer = new Designer(new ShapeFactory(parsers));
			//var canvases = from factory in _canvasFactories select factory.CreateCanvas(settings.W, settings.H);
			var draft = designer.CreateDraft(settings.Reader, settings.ErrorHandler);
			var painter = new Painter();

			var svgCanvas = new SVGCanvasWithSave(settings.W, settings.H);
			var dummyCanvas = new DummyCanvasWithSave(settings.W, settings.H);
			var bitmapCanvas = new BitmapCanvasWithSave(settings.W, settings.H);

			painter.DrawPicture(draft, svgCanvas);
			painter.DrawPicture(draft, dummyCanvas);
			painter.DrawPicture(draft, bitmapCanvas);

			svgCanvas.Save(settings.Output);
			dummyCanvas.Save(settings.Output);
			bitmapCanvas.Save(settings.Output);
		}
	}
}
