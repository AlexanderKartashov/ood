package menu.action.details;

import document.commands.ICommandsFactory;
import document.documentItem.ItemsCollection.IDocumentItemCollection;
import document.documentItem.paragraph.IMutableParagraph;
import document.documentItem.paragraph.IParagraph;
import hsitory.ICommand;
import hsitory.IHistory;

public class ChangeText extends ActionWithHistory {
    public ChangeText(IHistory history, ICommandsFactory commands, IDocumentItemCollection items, int position, String newText) {
        super(history);
        _commands = commands;

        IParagraph p = items.GetItem(position).GetParagraph();
        if (p == null)
        {
            throw new IllegalArgumentException(Integer.toString(position) + " item not a paragraph");
        }
        if (!(p instanceof IMutableParagraph))
        {
            throw new IllegalArgumentException(Integer.toString(position) + " is readonly paragraph");
        }
        _paragraph = (IMutableParagraph)p;

        _newText = newText;
    }

    @Override
    protected ICommand CreateCommand() {
        return _commands.EditText(_paragraph, _newText);
    }

    private final ICommandsFactory _commands;
    private final IMutableParagraph _paragraph;
    private final String _newText;
}
