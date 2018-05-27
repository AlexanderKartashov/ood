using MenuCommon;
using System.Text.RegularExpressions;

namespace GumBallMachineMenu
{
	public class EndParser : IActionParser
	{
		private readonly Regex _pattern = new Regex("^End$", RegexOptions.IgnoreCase);

		public string Help => "End";

		public dynamic Parse(string input)
		{
			if (_pattern.IsMatch(input))
			{
				return new End();
			}
			return null;
		}
	}
}
