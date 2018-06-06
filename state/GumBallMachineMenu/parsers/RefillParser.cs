using MenuCommon;
using System.Text.RegularExpressions;

namespace GumBallMachineMenu
{
	public class RefillParser : IActionParser
	{
		private readonly Regex _pattern = new Regex(@"^Refill\s+(\d+)$", RegexOptions.IgnoreCase);

		public string Help => "Add balls in machine <Refill N>";

		public dynamic Parse(string input)
		{
			var matches = _pattern.Match(input);
			if (matches.Success)
			{
				if (uint.TryParse(matches.Groups[1].Value, out uint balls))
				{
					if (balls != 0)
					{
						return new Refill() { Balls = balls };
					}
				}
			}
			return null;
		}
	}

}
