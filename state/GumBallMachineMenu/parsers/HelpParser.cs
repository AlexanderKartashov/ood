using MenuCommon;
using System.Text.RegularExpressions;

namespace GumBallMachineMenu
{
	public class HelpParser : IActionParser
	{
		private readonly Regex _pattern = new Regex("^Help$", RegexOptions.IgnoreCase);

		public string Help => "Print actions list <Help>";

		public dynamic Parse(string input)
		{
			if (_pattern.IsMatch(input))
			{
				return new Help();
			}
			return null;
		}
	}

}
