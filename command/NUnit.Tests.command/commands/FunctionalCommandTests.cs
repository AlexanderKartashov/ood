using NUnit.Framework;
using command.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;

namespace command.commands.Tests
{
	[TestFixture()]
	[Parallelizable]
	public class FunctionalCommandTests
	{
		[Test()]
		public void FunctionalCommandTestExecute()
		{
			var onExecute = Substitute.For<Action>();
			var onUnexecute = Substitute.For<Action>();
			var onDestroy = Substitute.For<Action>();
			var functionalCommand = new FunctionalCommand(onExecute, onUnexecute, onDestroy);
			functionalCommand.Execute();
			onExecute.Received(1);
			onUnexecute.DidNotReceiveWithAnyArgs();
			onDestroy.DidNotReceiveWithAnyArgs();
		}

		[Test()]
		public void FunctionalCommandTestUnexecute()
		{
			var onExecute = Substitute.For<Action>();
			var onUnexecute = Substitute.For<Action>();
			var onDestroy = Substitute.For<Action>();
			var functionalCommand = new FunctionalCommand(onExecute, onUnexecute, onDestroy);
			functionalCommand.Execute();
			onExecute.ClearReceivedCalls();
			functionalCommand.Unexecute();
			onExecute.DidNotReceiveWithAnyArgs();
			onUnexecute.Received(1);
			onDestroy.DidNotReceiveWithAnyArgs();
		}

		[Test()]
		public void FunctionalCommandTestDestroy()
		{
			var onExecute = Substitute.For<Action>();
			var onUnexecute = Substitute.For<Action>();
			var onDestroy = Substitute.For<Action>();
			var functionalCommand = new FunctionalCommand(onExecute, onUnexecute, onDestroy);
			onExecute.DidNotReceiveWithAnyArgs();
			onUnexecute.DidNotReceiveWithAnyArgs();
			onDestroy.Received(1);
		}
	}
}