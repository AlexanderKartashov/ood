using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.commands
{
	public class CommandError : Exception
	{
		public CommandError(string message)
			: base(message)
		{
		}
	}
}
