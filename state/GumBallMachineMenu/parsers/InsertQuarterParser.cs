﻿using MenuCommon;
using System.Text.RegularExpressions;

namespace GumBallMachineMenu
{
	internal class InsertQuarterParser : IActionParser
	{
		private readonly Regex _pattern = new Regex("^InsertQuarter$", RegexOptions.IgnoreCase);

		public string Help => "Insert quarter <InsertQuarter>";

		public dynamic Parse(string input)
		{
			if (_pattern.IsMatch(input))
			{
				return new InsertQuarter();
			}
			return null;
		}
	}

}
