using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using command.commandFactory;
using command.document;
using command.externals;
using command.history;
using command.storage;

namespace texteditor
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				var document = new Document();
				var fileSystem = new FileSystem();
				var htmlEncoder = new HtmlEncoder();

				using (var history = new History())
				using (var fileStorage = new FileStorage(fileSystem))
				{
					var documentItemFactoty = new DocumentItemFactory(fileStorage);
					var commandFactory = new CommandFactory(document, documentItemFactoty, history, fileStorage, fileSystem, htmlEncoder, Console.Out, Console.Out);
					UserInteractoin(Console.In, commandFactory);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		static void UserInteractoin(TextReader textReader, CommandFactory commandFactory)
		{
			string str = textReader.ReadLine();
			while (str != null && str.Length != 0)
			{
				commandFactory.ParseInput(str);
				str = textReader.ReadLine();
			}
		}
	}
}
