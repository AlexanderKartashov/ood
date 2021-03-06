﻿using MenuCommon;
using System.Text.RegularExpressions;

namespace GumBallMachineMenu
{
	internal class EjectQuartersParser : IActionParser
	{
		private readonly Regex _pattern = new Regex("^EjectQuarters$", RegexOptions.IgnoreCase);

		public string Help => "Eject all quarters <EjectQuarters>";

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
