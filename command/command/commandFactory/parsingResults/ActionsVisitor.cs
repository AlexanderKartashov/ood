using command.commands;
using command.document;
using command.document.visitors;
using command.externals;
using command.history;
using command.storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.commandFactory
{
	public class ActionsVisitor
	{
		private readonly IHistory _history;
		private readonly IHelpInfo _help;
		private readonly TextWriter _textWriter;
		private readonly IFileStorage _fileStorage;
		private readonly IFileSystem _fileSystem;
		private readonly IHtmlEncoder _htmlEncoder;

		public ActionsVisitor(IHistory history, IHelpInfo helpInfo, TextWriter textWriter, IFileStorage fileStorage, IFileSystem fileSystem, IHtmlEncoder htmlEncoder)
		{
			_history = history ?? throw new ArgumentNullException(nameof(history));
			_help = helpInfo ?? throw new ArgumentNullException(nameof(helpInfo));
			_textWriter = textWriter ?? throw new ArgumentNullException(nameof(textWriter));
			_fileStorage = fileStorage ?? throw new ArgumentNullException(nameof(fileStorage));
			_fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
			_htmlEncoder = htmlEncoder ?? throw new ArgumentNullException(nameof(htmlEncoder));
		}

		public void Visit(SaveAction save)
		{
			SaveVisitor visitor = new SaveVisitor(_fileStorage, _htmlEncoder, _fileSystem, save.PathToSave);
			using (var file = _fileSystem.CreateTextFile(visitor.AbsFilePath))
			{
				visitor.VisitDocument(save.Document, file);
			}
		}

		public void Visit(CommandContainer command)
		{
			command.Command.Execute();
			_history.AddCommand(command.Command);
		}

		public void Visit(ListAction action)
		{
			ListVisitor visitor = new ListVisitor();
			visitor.VisitDocument(action.Document, _textWriter);
		}

		public void Visit(ActionContainer action)
		{
			switch (action.SelectedAction)
			{
				case ActionContainer.Action.Undo:
					if (_history.CanUndo)
					{
						_history.Undo();
					}
					else
					{
						throw new CommandError("Undo operation not allowed");
					}
					break;
				case ActionContainer.Action.Redo:
					if (_history.CanRedo)
					{
						_history.Redo();
					}
					else
					{
						throw new CommandError("Redo operation not allowed");
					}
					break;
				case ActionContainer.Action.Help:
					_textWriter.WriteLine(_help.HelpText);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(action.SelectedAction));
			}
		}
	}
}
