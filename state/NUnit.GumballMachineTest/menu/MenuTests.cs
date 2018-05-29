using NUnit.Framework;
using MenuCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;

namespace MenuCommon.Tests
{
	[TestFixture()]
	[Parallelizable]
	public class MenuTests
	{
		private Menu _menu;
		private IErrorHandler _errorHandler;
		private ICommandSource _commandSource;
		private ICommandHandler _commandHandler;

		[SetUp]
		public void Setup()
		{
			_errorHandler = Substitute.For<IErrorHandler>();
			_menu = new Menu(_errorHandler);
			_commandHandler = Substitute.For<ICommandHandler>();
			_commandSource = Substitute.For<ICommandSource>();
		}

		[Test()]
		public void TestCommandProcessed()
		{
			var command1 = "command1";
			var command2 = "command2";
			_commandSource.Commands.Returns(new List<String> { command1, command2 });
			var parser1 = Substitute.For<IActionParser>();
			var parser2 = Substitute.For<IActionParser>();
			var cmd1 = new Command1();
			var cmd2 = new Command2();
			parser1.Parse(Arg.Is(command1)).Returns(cmd1);
			parser1.Parse(Arg.Is(command2)).Returns((Command1)null);
			parser2.Parse(Arg.Is(command1)).Returns((Command2)null);
			parser2.Parse(Arg.Is(command2)).Returns(cmd2);
			_commandHandler.Parsers.Returns(new List<IActionParser> { parser1, parser2 });
			_commandHandler.DoAction(Arg.Is(cmd1)).Returns(true);
			_commandHandler.DoAction(Arg.Is(cmd2)).Returns(true);

			Assert.That(() => _menu.Run(_commandSource, _commandHandler), Throws.Nothing);

			parser1.Received(1).Parse(Arg.Is(command1));
			parser2.Received(1).Parse(Arg.Is(command2));
			_commandHandler.Received(1).DoAction(cmd1);
			_commandHandler.Received(1).DoAction(cmd2);
			_errorHandler.DidNotReceive().InvalidCommand(Arg.Any<string>());
		}

		[Test()]
		public void TestStopIfCommandProcessorReturnsFalse()
		{
			var command1 = "command1";
			var command2 = "command2";
			_commandSource.Commands.Returns(new List<String> { command1, command2 });
			var parser1 = Substitute.For<IActionParser>();
			var parser2 = Substitute.For<IActionParser>();
			var cmd1 = new Command1();
			var cmd2 = new Command2();
			parser1.Parse(Arg.Is(command1)).Returns(cmd1);
			parser1.Parse(Arg.Is(command2)).Returns((Command1)null);
			parser2.Parse(Arg.Is(command1)).Returns((Command2)null);
			parser2.Parse(Arg.Is(command2)).Returns(cmd2);
			_commandHandler.Parsers.Returns(new List<IActionParser> { parser1, parser2 });
			_commandHandler.DoAction(Arg.Is(cmd1)).Returns(false);
			_commandHandler.DoAction(Arg.Is(cmd2)).Returns(true);

			Assert.That(() => _menu.Run(_commandSource, _commandHandler), Throws.Nothing);

			parser1.Received(1).Parse(Arg.Is(command1));
			parser2.DidNotReceive().Parse(Arg.Is(command2));
			_commandHandler.Received(1).DoAction(cmd1);
			_commandHandler.DidNotReceive().DoAction(cmd2);
			_errorHandler.DidNotReceive().InvalidCommand(Arg.Any<string>());
		}

		[Test()]
		public void TestCommandNotProcessed()
		{
			var command1 = "command1";
			_commandSource.Commands.Returns(new List<String> { command1 });
			var parser1 = Substitute.For<IActionParser>();
			parser1.Parse(Arg.Is(command1)).Returns((Command1)null);
			_commandHandler.Parsers.Returns(new List<IActionParser> { parser1 });
			Assert.That(() => _menu.Run(_commandSource, _commandHandler), Throws.Nothing);

			parser1.Received(1).Parse(Arg.Is(command1));
			_commandHandler.DidNotReceive().DoAction(Arg.Any<dynamic>());
			_errorHandler.Received(1).InvalidCommand($"Unrecognised command {command1}");
		}

		[Test()]
		public void TestShouldReturnIfCommandNull()
		{
			_commandSource.Commands.Returns(new List<String>{ null });
			Assert.That(() => _menu.Run(_commandSource, _commandHandler), Throws.Nothing);
			var p = _commandHandler.DidNotReceiveWithAnyArgs().Parsers;
			_errorHandler.DidNotReceive().InvalidCommand(Arg.Any<string>());
		}

		[Test()]
		public void TestShouldReturnIfCommandEmpty()
		{
			_commandSource.Commands.Returns(new List<String> { "" });
			Assert.That(() => _menu.Run(_commandSource, _commandHandler), Throws.Nothing);
			var p =_commandHandler.DidNotReceiveWithAnyArgs().Parsers;
			_errorHandler.DidNotReceive().InvalidCommand(Arg.Any<string>());
		}

		[Test()]
		public void TestShouldReturnIfCommandListEmpty()
		{
			_commandSource.Commands.Returns(new List<String> { });
			Assert.That(() => _menu.Run(_commandSource, _commandHandler), Throws.Nothing);
			var p = _commandHandler.DidNotReceiveWithAnyArgs().Parsers;
			_errorHandler.DidNotReceive().InvalidCommand(Arg.Any<string>());
		}

		class Command1
		{ }

		class Command2
		{ }
	}
}