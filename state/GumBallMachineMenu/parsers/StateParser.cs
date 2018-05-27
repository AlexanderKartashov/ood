using MenuCommon;
using System.Text.RegularExpressions;

namespace GumBallMachineMenu
{
	public class StateParser : IActionParser
	{
		private readonly Regex _pattern = new Regex("^State$", RegexOptions.IgnoreCase);

		public string Help => "Show current machine state";

		public dynamic Parse(string input)
		{
			if (_pattern.IsMatch(input))
			{
				return new State();
			}
			return null;
		}
	}
}
