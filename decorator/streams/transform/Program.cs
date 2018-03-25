using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace transform
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				var options = new CommandLineOptions();
				Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed(x => Process(x));
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		static void Process(CommandLineOptions options)
		{


			if (options.Compress)
			{

			}
			if (options.Decompress)
			{

			}
			if (options.Encrypt.HasValue)
			{

			}
			if (options.Decrypt.HasValue)
			{

			}
		}
	}

	class CommandLineOptions
	{
		[Option('e', "encrypt", Required = false)]
		public int? Encrypt { get; set; }

		[Option('d', "decrypt", Required = false)]
		public int? Decrypt { get; set; }

		[Option('c', "compress", Required = false, Default = false)]
		public bool Compress { get; set; }

		[Option('u', "decompress", Required = false, Default = false)]
		public bool Decompress { get; set; }

		[Option('i', "input", Required = true)]
		public string Input { get; set; }

		[Option('o', "output", Required = true)]
		public string Output { get; set; }
	}

}
