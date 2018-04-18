using NUnit.Framework;
using NSubstitute;
using command.history;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using command.commands;

namespace command.history.Tests
{
	[TestFixture]
	[Parallelizable]
	public class HistoryTests
	{
		private History _history;

		[SetUp]
		public void SetUp()
		{
			_history = new History(5);
		}

		[Test]
		public void AddCommandTest()
		{
			Assert.That(() => _history.AddCommand(null), Throws.ArgumentNullException);

			var command = Substitute.For<AbstractCommand>();
			bool? executed = null;
			command.IsExecuted.Returns(x => { return executed; });

			Assert.That(() => _history.AddCommand(command), Throws.ArgumentException);

			command.IsExecuted.Returns(false);
			Assert.That(() => _history.AddCommand(command), Throws.ArgumentException);

			var command1 = Substitute.For<AbstractCommand>();
			command1.IsExecuted.Returns(true);
			Assert.That(() => _history.AddCommand(command1), Throws.Nothing); // 1 command

			var command2 = Substitute.For<AbstractCommand>();
			command2.IsExecuted.Returns(true);
			Assert.That(() => _history.AddCommand(command2), Throws.Nothing); // 2 commands

			var command3 = Substitute.For<AbstractCommand>();
			command3.IsExecuted.Returns(true);
			Assert.That(() => _history.AddCommand(command3), Throws.Nothing); // 3 commands

			var command4 = Substitute.For<AbstractCommand>();
			command4.IsExecuted.Returns(true);
			Assert.That(() => _history.AddCommand(command4), Throws.Nothing); // 4 commands

			var command5 = Substitute.For<AbstractCommand>();
			command5.IsExecuted.Returns(true);
			Assert.That(() => _history.AddCommand(command5), Throws.Nothing); // 5 commands

			command1.DidNotReceive().Dispose();
			command2.DidNotReceive().Dispose();
			command3.DidNotReceive().Dispose();
			command4.DidNotReceive().Dispose();
			command5.DidNotReceive().Dispose();

			var command6 = Substitute.For<AbstractCommand>();
			command6.IsExecuted.Returns(true);
			Assert.That(() => _history.AddCommand(command6), Throws.Nothing); // 5 commands, command 1 disposed

			command1.Received().Dispose();

			Assert.That(() => _history.Undo(), Throws.Nothing); // command6
			Assert.That(() => _history.Undo(), Throws.Nothing); // command5

			var command7 = Substitute.For<AbstractCommand>();
			command7.IsExecuted.Returns(true);
			Assert.That(() => _history.AddCommand(command7), Throws.Nothing); // 3 commands

			command5.Received(1).Dispose();
			command6.Received(1).Dispose();
			command2.DidNotReceive().Dispose();
			command3.DidNotReceive().Dispose();
			command4.DidNotReceive().Dispose();
		}

		[Test]
		public void DisposeTest()
		{
			var command1 = Substitute.For<AbstractCommand>();
			command1.IsExecuted.Returns(true);
			Assert.That(() => _history.AddCommand(command1), Throws.Nothing); // 1 command

			var command2 = Substitute.For<AbstractCommand>();
			command2.IsExecuted.Returns(true);
			Assert.That(() => _history.AddCommand(command2), Throws.Nothing); // 2 commands

			var command3 = Substitute.For<AbstractCommand>();
			command3.IsExecuted.Returns(true);
			Assert.That(() => _history.AddCommand(command3), Throws.Nothing); // 3 commands

			Assert.That(() => _history.Dispose(), Throws.Nothing);

			command1.Received(1).Dispose();
			command2.Received(1).Dispose();
			command3.Received(1).Dispose();
		}

		[Test]
		public void RedoTest()
		{
			Assert.That(() => _history.Redo(), Throws.InvalidOperationException);

			var command1 = Substitute.For<AbstractCommand>();
			command1.IsExecuted.Returns(true);
			Assert.That(() => _history.AddCommand(command1), Throws.Nothing); // 1 command
			Assert.That(() => _history.Redo(), Throws.InvalidOperationException);

			var command2 = Substitute.For<AbstractCommand>();
			command2.IsExecuted.Returns(true);
			Assert.That(() => _history.AddCommand(command2), Throws.Nothing); // 2 commands
			Assert.That(() => _history.Redo(), Throws.InvalidOperationException);

			Assert.That(() => _history.Undo(), Throws.Nothing);

			Assert.That(() => _history.Redo(), Throws.Nothing);
			command2.Received(1).Execute();
		}

		[Test]
		public void UndoTest()
		{
			Assert.That(() => _history.Undo(), Throws.InvalidOperationException);

			var command1 = Substitute.For<AbstractCommand>();
			command1.IsExecuted.Returns(true);
			Assert.That(() => _history.AddCommand(command1), Throws.Nothing); // 1 command

			var command2 = Substitute.For<AbstractCommand>();
			command2.IsExecuted.Returns(true);
			Assert.That(() => _history.AddCommand(command2), Throws.Nothing); // 2 commands

			Assert.That(() => _history.Undo(), Throws.Nothing);
			command2.Received(1).Unexecute();
		}

		[Test]
		public void CanUndoTest()
		{
			Assert.That(_history.CanUndo, Is.False);

			var command1 = Substitute.For<AbstractCommand>();
			command1.IsExecuted.Returns(true);
			Assert.That(() => _history.AddCommand(command1), Throws.Nothing); // 1 command

			Assert.That(_history.CanUndo, Is.True);

			var command2 = Substitute.For<AbstractCommand>();
			command2.IsExecuted.Returns(true);
			Assert.That(() => _history.AddCommand(command2), Throws.Nothing); // 2 commands
			Assert.That(_history.CanUndo, Is.True);

			Assert.That(() => _history.Undo(), Throws.Nothing);
			Assert.That(_history.CanUndo, Is.True);

			Assert.That(() => _history.Undo(), Throws.Nothing);
			Assert.That(_history.CanUndo, Is.False);

			Assert.That(() => _history.Redo(), Throws.Nothing);
			Assert.That(_history.CanUndo, Is.True);

			Assert.That(() => _history.Redo(), Throws.Nothing);
			Assert.That(_history.CanUndo, Is.True);
		}

		[Test]
		public void CanRedoTest()
		{
			Assert.That(_history.CanRedo, Is.False);

			var command1 = Substitute.For<AbstractCommand>();
			command1.IsExecuted.Returns(true);
			Assert.That(() => _history.AddCommand(command1), Throws.Nothing); // 1 command
			Assert.That(_history.CanRedo, Is.False);

			var command2 = Substitute.For<AbstractCommand>();
			command2.IsExecuted.Returns(true);
			Assert.That(() => _history.AddCommand(command2), Throws.Nothing); // 2 commands
			Assert.That(_history.CanRedo, Is.False);

			Assert.That(() => _history.Undo(), Throws.Nothing);
			Assert.That(_history.CanRedo, Is.True);

			Assert.That(() => _history.Undo(), Throws.Nothing);
			Assert.That(_history.CanRedo, Is.True);

			Assert.That(() => _history.Redo(), Throws.Nothing);
			Assert.That(_history.CanRedo, Is.True);

			Assert.That(() => _history.Redo(), Throws.Nothing);
			Assert.That(_history.CanRedo, Is.False);
		}
	}
}