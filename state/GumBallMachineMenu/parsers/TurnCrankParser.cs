using MenuCommon;
using System.Text.RegularExpressions;

namespace GumBallMachineMenu
{
	public class TurnCrankParser : IActionParser
	{
		private readonly Regex _pattern = new Regex("^TurnCrank$", RegexOptions.IgnoreCase);

		public string Help => "Turn crank <TurnCrank>";

		public dynamic Parse(string input)
		{
			if (_pattern.IsMatch(input))
			{
				return new TurnCrank();
			}
			return null;
		}
	}

}
