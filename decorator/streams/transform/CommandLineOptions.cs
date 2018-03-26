using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace transform
{
	class CommandLineOptions
	{
		[Option('e', "encrypt", Required = false, MetaValue = "<keys>")]
		public IEnumerable<int> Encrypt { get; set; }

		[Option('d', "decrypt", Required = false, MetaValue = "<keys>")]
		public IEnumerable<int> Decrypt { get; set; }

		[Option('c', "compress", Required = false, Default = false)]
		public bool Compress { get; set; }

		[Option('u', "decompress", Required = false, Default = false)]
		public bool Decompress { get; set; }

		[Option('i', "input", Required = false, MetaValue = "<input file>", HelpText = "if value does not exists, will be generated random bytes sequence")]
		public string Input { get; set; }

		[Option('o', "output", Required = true, MetaValue = "<output file>")]
		public string Output { get; set; }
	}
}
