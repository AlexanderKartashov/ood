namespace MenuCommon
{
	public interface IActionParser : IActionHelp
	{
		dynamic Parse(string input); // returns null if action not recognized
	}
}
