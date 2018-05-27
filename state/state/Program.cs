using GumBallMachineMenu;
using GumBallMachineState;
using MenuCommon;
using System;

namespace state
{
	class Program
	{
		static void Main(string[] args)
		{
			var modules = new GumballMachineModules(Console.Out, Console.In, Console.Out);
			var gumballMachine = new GumballMachine(0, 5, modules.MachineErrorHandler, modules.Logger);
			var commandHandler = new CommandHandler(gumballMachine, gumballMachine, modules.Logger);
			var menu = new Menu(modules.MenuErrorHandler);
			menu.Run(modules.CommandSource, commandHandler);
		}
	}
}
