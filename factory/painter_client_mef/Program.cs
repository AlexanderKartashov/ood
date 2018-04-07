using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using painter;
using painter_clinet_common;
using painter_declarations;

namespace painter_client_mef
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
			DrawPictureMef draw = new DrawPictureMef();
			draw.DrawPicture(options);
		}

		class DrawPictureMef
		{
			[ImportMany(typeof(IShapeParser))]
			public IEnumerable<IShapeParser> Parsers { get; set; }

			[ImportMany(typeof(ICanvasFactory))]
			public IEnumerable<ICanvasFactory> CanvasFactories { get; set; }

			public void DrawPicture(CommandLineOptions options)
			{
				CommandLineOptionsProcessor processor = new CommandLineOptionsProcessor();
				var settings = processor.Process(options);

				var catalog = new DirectoryCatalog(Environment.CurrentDirectory);
				var compositionContainer = new CompositionContainer(catalog);
				compositionContainer.ComposeParts(this);

				var factories = CanvasFactories.GetEnumerator();
				while (factories.MoveNext())
				{
					PictureDraw.DrawPictureOnCanvas(factories.Current, settings, Parsers);
				}
			}
		}

	}
}
