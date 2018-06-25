package menu.action.details;

import document.commands.ICommandsFactory;
import document.documentItem.ItemsCollection.IMutableDocumentItemCollection;
import hsitory.ICommand;
import hsitory.IHistory;

public class DeleteItem extends ActionWithHistory {

    public DeleteItem(IHistory history, IMutableDocumentItemCollection items, int pos, ICommandsFactory commands) {
        super(history);
        _items = items;
        _pos = pos;
        _commands = commands;
    }

    @Override
    protected ICommand CreateCommand() {
        return _commands.DeleteItem(_items, _pos);
    }

    private final IMutableDocumentItemCollection _items;
    private final int _pos;
    private final ICommandsFactory _commands;
}
