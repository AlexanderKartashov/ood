using command.commands;
using command.document.visitors;
using command.externals;
using command.history;
using command.storage;
using System;
using System.IO;

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
		private readonly BuilderDirector _director = new BuilderDirector();

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
			using (var builder = new SaveBuilder(_fileStorage, _htmlEncoder, _fileSystem, save.PathToSave))
			{
				_director.Build(builder, save.Document);
			}
		}

		public void Visit(CommandContainer command)
		{
			command.Command.Execute();
			_history.AddCommand(command.Command);
		}

		public void Visit(ListAction list)
		{
			var builder = new ListBuilder(_textWriter);
			_director.Build(builder, list.Document);
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
