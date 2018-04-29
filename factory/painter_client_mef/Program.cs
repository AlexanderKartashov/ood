using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using CommandLine;
using painter.sdk;
using painter_clinet.utils;
using painter_clinet_common;
using System.Linq;
using canvas;

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
			new MefCompsition().Run(options);
		}

		class MefCompsition
		{
			[ImportMany(typeof(IShapeParser))] IEnumerable<IShapeParser> Parsers { get; set; }
			[ImportMany(typeof(ICanvasFactory))] IEnumerable<ICanvasFactory> CanvasFactories { get; set; }
			[ImportMany(typeof(ICanvasVisitor))] IEnumerable<ICanvasVisitor> CanvasVisitors { get; set; }

			public void Run(CommandLineOptions options)
			{
				var catalog = new DirectoryCatalog(Environment.CurrentDirectory);
				var compositionContainer = new CompositionContainer(catalog);
				compositionContainer.ComposeParts(this);

				CommandLineOptionsProcessor processor = new CommandLineOptionsProcessor();
				var settings = processor.Process(options);

				CanvasVisitors.Cast<FileCanvasPresenter>().ToList().ForEach(x => { x.Directory = settings.Output; });

				DrawPicture drawPicture = new DrawPicture(Parsers, CanvasFactories, CanvasVisitors);
				drawPicture.Draw(settings);
			}
		}
	}
}
