using MenuCommon;
using System.Text.RegularExpressions;

namespace GumBallMachineMenu
{
	public class EjectQuartersParser : IActionParser
	{
		private readonly Regex _pattern = new Regex("^EjectQuarters$", RegexOptions.IgnoreCase);

		public string Help => "Eject all quarter";

		public dynamic Parse(string input)
		{
			if (_pattern.IsMatch(input))
			{
				return new EjectQuarters();
			}
			return null;
		}
	}

}
