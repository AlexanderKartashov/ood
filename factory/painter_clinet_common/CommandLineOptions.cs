using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace painter_clinet_common
{
	public class CommandLineOptions
	{
		[Option('i', "input", HelpText = "Path to shapes list", Required = false, MetaValue = "<shapes descriptions file>")]
		public string Input { get; set; }

		[Option('o', "output", HelpText = "Path to output files directory", Required = true, MetaValue = "<directory path>")]
		public string Output { get; set; }

		[Option('w', "width", HelpText = "Canvas width", Required = false, Default = 1280, MetaValue = "<value>")]
		public int Width { get; set; }

		[Option('h', "height", HelpText = "Canvas height", Required = false, Default = 720, MetaValue = "<value>")]
		public int Height { get; set; }
	}
}
