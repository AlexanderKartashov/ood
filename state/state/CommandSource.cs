using MenuCommon;
using System;
using System.Collections.Generic;
using System.IO;

namespace state
{
	class CommandSource : ICommandSource
	{
		private readonly TextReader _input;

		public CommandSource(TextReader input) => _input = input ?? throw new ArgumentNullException(nameof(input));

		public IEnumerable<string> Commands
		{
			get
			{
				string currentLine;
				while ((currentLine = _input.ReadLine()) != null)
				{
					yield return currentLine;
				}
			}
		}
	}
}
