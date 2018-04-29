using command.commands;
using command.document;
using command.externals;
using command.history;
using command.storage;
using System;
using System.IO;

namespace command.commandFactory
{
	public class CommandFactory
	{
		private readonly IDocument _document;
		private readonly IDocumentItemFactory _documentItemFactory;
		private readonly IHistory _history;
		private readonly IFileSystem _fileSystem;
		private readonly IHtmlEncoder _encoder;
		private readonly InputParser _inputParser;
		private readonly IFileStorage _fileStorage;
		private readonly TextWriter _textWriter;
		private readonly TextWriter _errorLogger;

		public CommandFactory(IDocument document, IDocumentItemFactory documentItemFactory, 
			IHistory history, IFileStorage fileStorage,
			IFileSystem fileSystem, IHtmlEncoder encoder,
			TextWriter textWriter, TextWriter errorLogger)
		{
			_document = document ?? throw new ArgumentNullException(nameof(document));
			_documentItemFactory = documentItemFactory ?? throw new ArgumentNullException(nameof(documentItemFactory));
			_history = history ?? throw new ArgumentNullException(nameof(history));
			_textWriter = textWriter ?? throw new ArgumentNullException(nameof(textWriter));
			_errorLogger = errorLogger ?? throw new ArgumentNullException(nameof(errorLogger));
			_fileStorage = fileStorage ?? throw new ArgumentNullException(nameof(fileStorage));
			_fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
			_encoder = encoder ?? throw new ArgumentNullException(nameof(encoder));
			_inputParser = new InputParser(_document, _documentItemFactory);
		}

		public void ParseInput(string command)
		{
			try
			{
				var parsedCommand = _inputParser.ParseInput(command) ?? throw new InvalidOperationException(nameof(command));
				ActionsVisitor visitor = new ActionsVisitor(_history, _inputParser, _textWriter, _fileStorage, _fileSystem, _encoder);
				visitor.Visit((dynamic)parsedCommand);
			}
			catch(CommandError err)
			{
				_errorLogger.WriteLine(err.Message);
			}
			catch (Exception)
			{
				_errorLogger.WriteLine($"Invalid command {command}");
			}
		}
	}
}
