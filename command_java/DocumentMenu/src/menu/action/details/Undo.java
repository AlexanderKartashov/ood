package menu.action.details;

import hsitory.IHistory;
import menu.IAction;

public class Undo implements IAction {
    public Undo(IHistory history) {
        _history = history;
    }

    @Override
    public void Perform() throws Exception {
        _history.Undo();
    }

    private final IHistory _history;
}
