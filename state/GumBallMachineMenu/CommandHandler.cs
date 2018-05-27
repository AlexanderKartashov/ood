using GumBallMachineCommon;
using MenuCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace GumBallMachineMenu
{
	public class CommandHandler : ICommandHandler
	{
		private readonly Visitor _visitor;

		public CommandHandler(IGumballMachine machine, IGumballMachineMaintenance maintenance,
			IActionsLogger logger) => _visitor = new Visitor(machine, maintenance, logger);

		public IEnumerable<IActionParser> Parsers => _visitor.Parsers;

		public bool DoAction(dynamic action) => _visitor.Visit(action);

		private class Visitor
		{
			private readonly IGumballMachine _machine;
			private readonly IGumballMachineMaintenance _machineMaintenance;
			private readonly IActionsLogger _logger;

			public IEnumerable<IActionParser> Parsers { get; } = new List<IActionParser> {
				new EjectQuartersParser(),
				new InsertQuarterParser(),
				new TurnCrankParser(),
				new RefillParser(),
				new StateParser(),
				new EndParser(),
				new HelpParser()
			};

			public Visitor(IGumballMachine machine,
				IGumballMachineMaintenance maintenance,
				IActionsLogger logger)
			{
				_machine = machine ?? throw new ArgumentNullException(nameof(machine));
				_machineMaintenance = maintenance ?? throw new ArgumentNullException(nameof(maintenance));
				_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			}

			public bool Visit(InsertQuarter action)
			{
				_machine.InsertQuarter();
				return true;
			}

			public bool Visit(EjectQuarters action)
			{
				_machine.EjectQuarter();
				return true;
			}

			public bool Visit(TurnCrank action)
			{
				_machine.TurnCrank();
				return true;
			}

			public bool Visit(Refill action)
			{
				_machineMaintenance.RefillBalls(action.Balls);
				return true;
			}

			public bool Visit(Help action)
			{
				var sb = new StringBuilder();
				foreach(var act in Parsers)
				{
					sb.AppendLine(act.Help);
				}
				_logger.Log(sb.ToString());
				return true;
			}

			public bool Visit(State action)
			{
				_logger.Log(_machine.ToString());
				return true;
			}

			public bool Visit(End action) => false;
		}
	}
}
