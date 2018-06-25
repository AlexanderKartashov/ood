package menu.action.details;

import document.commands.ICommandsFactory;
import document.documentItem.title.IMutableDocumentTitle;
import hsitory.ICommand;
import hsitory.IHistory;

public class SetTitle  extends ActionWithHistory {

    public SetTitle(String newTitle, IMutableDocumentTitle documentTitle, IHistory history, ICommandsFactory commands) {
        super(history);
        _newTitle = newTitle;
        _documentTitle = documentTitle;
        _commands = commands;
    }

    @Override
    protected ICommand CreateCommand() {
        return _commands.SetTitle(_documentTitle, _newTitle);
    }

    private final String _newTitle;
    private final IMutableDocumentTitle _documentTitle;
    private final ICommandsFactory _commands;
}
