using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuCommon
{
	public interface ICommandHandler
	{
		// returns true if continue commands processing
		bool DoAction(dynamic action);
		IEnumerable<IActionParser> Parsers { get; }
	}
}
