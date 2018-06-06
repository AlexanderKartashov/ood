using System;

namespace MenuCommon
{
	public class Menu
	{
		private readonly IErrorHandler _errorHandler;

		public Menu(IErrorHandler errorHandler) => _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));

		public void Run(ICommandSource commandSource, ICommandHandler handler)
		{
			foreach (var command in commandSource.Commands)
			{
				if (command == null || command.Length == 0)
				{
					// command finished
					return;
				}

				var commandHandled = false;
				foreach (var parser in handler.Parsers)
				{
					try
					{
						var parsedCommand = parser.Parse(command);
						if (!ReferenceEquals(parsedCommand, null))
						{
							if (handler.DoAction(parsedCommand))
							{
								commandHandled = true;
								break;
							}
							else
							{
								return;
							}
						}
					}
					catch (Exception ex)
					{
						_errorHandler.InvalidCommand(ex.Message);
					}
				}

				if (!commandHandled)
				{
					// parser not found
					_errorHandler.InvalidCommand($"Unrecognised command {command}");
				}
			}
		}
	}
}
