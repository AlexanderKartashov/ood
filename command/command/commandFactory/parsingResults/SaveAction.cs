using command.document;

namespace command.commandFactory
{
	public struct SaveAction
	{
		public string PathToSave { get; set; }
		public IDocument Document { get; set; }
	}
}
