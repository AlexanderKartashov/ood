using GumBallMachineCommon;
using System;
using System.IO;

namespace state
{
	class TextWriterAdapter : MenuCommon.IErrorHandler, IActionsLogger, GumBallMachineCommon.IErrorHandler
	{
		private readonly TextWriter _loggerWriter;
		private readonly TextWriter _errorWriter;

		public TextWriterAdapter(TextWriter loggerWriter, TextWriter errorWriter)
		{
			_loggerWriter = loggerWriter ?? throw new ArgumentNullException(nameof(loggerWriter));
			_errorWriter = errorWriter ?? throw new ArgumentNullException(nameof(errorWriter));
		}

		public void InvalidAction(string message)
		{
			_errorWriter.WriteLine(message);
		}

		public void InvalidCommand(string message)
		{
			_errorWriter.WriteLine(message);
		}

		public void Log(string action)
		{
			_loggerWriter.WriteLine(action);
		}
	}
}
