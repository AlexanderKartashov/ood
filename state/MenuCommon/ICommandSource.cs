using System.Collections.Generic;

namespace MenuCommon
{
	public interface ICommandSource
	{
		IEnumerable<string> Commands { get; }
	}
}
