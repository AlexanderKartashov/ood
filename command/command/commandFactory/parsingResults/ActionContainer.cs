using command.document;

namespace command.commandFactory
{
	public class ActionContainer
	{
		public enum Action
		{
			Undo,
			Redo,
			Help
		}
		public Action SelectedAction { get; set; }
		public IDocument Document { get; set; } = null;
	}
}
