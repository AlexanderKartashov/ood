package menu.action.details;

import hsitory.IHistory;
import menu.IAction;

public class Redo implements IAction {
    public Redo(IHistory history) {
        _history = history;
    }

    @Override
    public void Perform() throws Exception {
        _history.Redo();
    }

    private final IHistory _history;
}
