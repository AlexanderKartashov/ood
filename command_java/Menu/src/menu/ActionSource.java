package menu;

import java.io.IOException;

public abstract class ActionSource implements IActionSource {

    protected ActionSource(IActionsParser parser) {
        _parser = parser;
    }

    public final IAction GetNextAction() throws IOException {
        final String action = GetActionDescription();
        return _parser.ParseAction(action);
    }

    protected abstract String GetActionDescription() throws IOException;

    private final IActionsParser _parser;
}
