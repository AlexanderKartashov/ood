using MenuCommon;
using System.Text.RegularExpressions;

namespace GumBallMachineMenu
{
	internal class EndParser : IActionParser
	{
		private readonly Regex _pattern = new Regex("^End$", RegexOptions.IgnoreCase);

		public string Help => "End <End>";

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
