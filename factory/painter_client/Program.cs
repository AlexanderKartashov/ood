using System;
using CommandLine;
using System.Collections.Generic;
using canvas;
using painter.parsers;
using painter_clinet_common;
using painter.sdk;
using painter_clinet.utils;

namespace painter_client
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
			var visitors = new List<ICanvasVisitor>() {
				new FileCanvasPresenter() { Directory = settings.Output }
			};

			var draw = new DrawPicture(parsers, factories, visitors);
			draw.Draw(settings);
		}
	}
}
