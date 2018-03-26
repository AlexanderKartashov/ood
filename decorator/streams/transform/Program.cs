using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using streams;

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
			var streamsDecorator = new StreamsDecorator();
			(IInputDataStream input, IOutputDataStream output) = streamsDecorator.ProccessCommandLineOptions(options);

			var pipeline = new StreamsPipeline();
			pipeline.Pipeline(input, output);
		}
	}
}
