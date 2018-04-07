using System;
using CommandLine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using canvas;
using painter;
using painter.parsers;
using painter.shapes;
using painter_clinet_common;
using painter_declarations;

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
			var canvasFactory = new SvgCanvasFactory();
			var parsers = new List<IShapeParser>() {
				new TriangleParser(),
				new RectangleParser(),
				new RectangularPolygonParser(),
				new EllipseParser()
			};
			PictureDraw.DrawPictureOnCanvas(canvasFactory, settings, parsers);
		}
	}
}
