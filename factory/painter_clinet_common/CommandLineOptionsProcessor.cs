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
			public TextReader reader { get; set; }
			public String output { get; set; }
			public int w { get; set; }
			public int h { get; set; }
		}

		public Options Process(CommandLineOptions options)
		{
			TextReader textReader = null;
			if (options.Input != null)
			{
				textReader = File.OpenText(GetAbsolutePath(options.Input));
			}
			else
			{
				textReader = Console.In;
			}

			string output = GetAbsolutePath(options.Output);
			if (!Directory.Exists(output))
			{
				Directory.CreateDirectory(output);
			}

			return new Options {
				reader = textReader,
				output = output,
				w = options.Width,
				h = options.Height
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
