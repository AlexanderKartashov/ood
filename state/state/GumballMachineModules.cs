using GumBallMachineCommon;
using MenuCommon;
using System.IO;

namespace state
{
	class GumballMachineModules
	{
		private readonly TextWriterAdapter _loggerAndErrorHandlerAdapater;
		private readonly CommandSource _commandSource;

		public GumballMachineModules(TextWriter output, TextReader input, TextWriter error)
		{
			_loggerAndErrorHandlerAdapater = new TextWriterAdapter(output, error);
			_commandSource = new CommandSource(input);
		}

		public MenuCommon.IErrorHandler MenuErrorHandler => _loggerAndErrorHandlerAdapater;
		public GumBallMachineCommon.IErrorHandler MachineErrorHandler => _loggerAndErrorHandlerAdapater;
		public IActionsLogger Logger => _loggerAndErrorHandlerAdapater;
		public ICommandSource CommandSource => _commandSource;
	}
}
