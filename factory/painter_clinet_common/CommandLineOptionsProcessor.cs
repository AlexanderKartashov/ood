using painter.sdk;
using painter_declarations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter_clinet_common
{
	public class CommandLineOptionsProcessor
	{
		public struct Options
		{
			public TextReader Reader { get; set; }
			public ErrorHandler ErrorHandler { get; set; }
			public String Output { get; set; }
			public int W { get; set; }
			public int H { get; set; }
		}

		public Options Process(CommandLineOptions options)
		{
			TextReader textReader = Console.In;
			ErrorHandler errorHandler = new ErrorHandler(Console.Out);
			if (options.Input != null)
			{
				textReader = File.OpenText(GetAbsolutePath(options.Input));
			}

			string output = GetAbsolutePath(options.Output);
			if (!Directory.Exists(output))
			{
				Directory.CreateDirectory(output);
			}

			return new Options {
				Reader = textReader,
				ErrorHandler = errorHandler,
				Output = output,
				W = options.Width,
				H = options.Height
			};
		}

		string GetAbsolutePath(string path)
		{
			if (Path.IsPathRooted(path))
			{
				return path;
			}

			return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
		}
	}
}
